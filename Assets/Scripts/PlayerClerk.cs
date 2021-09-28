using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectPool))]
public class PlayerClerk : MonoBehaviour
{
    public class PlayerClerkTask
    {
        public GameObject requestCustomer = null;
        public GameObject receiverCustomer = null;
        public bool completed = false;
        public float completionTime = 0.0f;

        public PlayerClerkTask(GameObject requestCustomer, GameObject receiverCustomer, float completionTime)
        {
            this.requestCustomer = requestCustomer;
            this.receiverCustomer = receiverCustomer;
            this.completionTime = completionTime;
        }
    }

    private PlayerClerkTask currentTask = null;
    private GameObjectPool pool = null;
    private Cable currentCable = null;

    private void Awake()
    {
        pool = GetComponent<GameObjectPool>();
    }

    public bool ScheduleNewTask(PlayerClerkTask task)
    {
        if (currentTask != null) return false;
        currentTask = task;
        SetupNewCable(task.requestCustomer.transform);
        currentTask.completionTime = CalculateMaxCompletionTime(task);
        Invoke(nameof(LoseTask), task.completionTime);
        return true;
    }

    private float CalculateMaxCompletionTime(PlayerClerkTask task)
    {
        //Min time to get from point a to point b + an error margin
        return (Vector3.Distance(task.requestCustomer.transform.position, task.receiverCustomer.transform.position) / 5f) + 3f;
    }

    private bool CheckCurrentTaskForCompletion(GameObject attemptReceiver)
    {
        return currentTask != null && currentTask.receiverCustomer == attemptReceiver;
    }

    public bool CompleteCurrentTask( GameObject attemptReceiver)
    {
        if (!CheckCurrentTaskForCompletion(attemptReceiver)) return false;
        currentCable.BTransform = attemptReceiver.transform;
        currentCable = null;
        currentTask = null;
        CancelInvoke(nameof(LoseTask));
        return true;
    }

    private void SetupNewCable(Transform a)
    {
        currentCable = pool.AllocateObject<Cable>(0, Vector3.zero);
        currentCable.ATransform = a;
        currentCable.BTransform = transform;
        currentCable.gameObject.SetActive(true);
    }

    private void LoseTask()
    {
        // signal current tasks' request actor to cancel the service
        currentTask?.requestCustomer.GetComponent<Customer>().CancelService();
    }
}
