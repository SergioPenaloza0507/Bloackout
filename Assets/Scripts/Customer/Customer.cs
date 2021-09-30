using System;
using UnityEngine;

public class Customer : MonoBehaviour
{   
    private PlayerClerk.PlayerClerkTask currentTask = null;

    public PlayerClerk.PlayerClerkTask CurrentTask
    {
        get => currentTask;
        set => currentTask = value;
    }

    private bool ThisRequest =>currentTask != null && currentTask.requestCustomer == gameObject;
    private bool ThisReceiver =>currentTask != null && currentTask.receiverCustomer == gameObject;

    private void Awake()
    {
        Invoke(nameof(CancelService), 5f);        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerClerk player))
        {
            if (ThisRequest)
            {
                if (player.ScheduleNewTask(currentTask))
                {
                    CancelInvoke(nameof(CancelService));
                }
            }
            else if (ThisReceiver)
            {
                if (player.CompleteCurrentTask(gameObject))
                {
                    //Signal game manager to heal player
                }
            }
        }
    }

    public void CancelService()
    {
        if (ThisRequest)
        {
            currentTask.receiverCustomer.SetActive(false);
            gameObject.SetActive(false);
            //Signal game manager to damage player
        }
    }
}
