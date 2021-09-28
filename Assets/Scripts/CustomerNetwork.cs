using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GameObjectPool))]
public class CustomerNetwork : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ParticleSystem.MinMaxCurve radius;
    [SerializeField] private ParticleSystem.MinMaxCurve receiverRadius;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnFrequency;

    private float spawnTimer;
    private float currentSpawnTime;
    private GameObjectPool pool;

    private List<Customer> customers;
    private List<List<int>> connections;

    private void Awake()
    {
        pool = GetComponent<GameObjectPool>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnTime)
        {
            SpawnAtRandomRadius();
            currentSpawnTime = spawnFrequency.Evaluate(Random.Range(0f, 1f));
            spawnTimer = 0f;
        }
    }

    private void SpawnAtRandomRadius()
    {
        Vector2 randomInCircleA = Random.insideUnitCircle.normalized;
        Vector2 randomInCircleB = Random.insideUnitCircle.normalized;
        Vector3 positionA = playerTransform.position + new Vector3(randomInCircleA.x, 0, randomInCircleA.y) * radius.Evaluate(Random.Range(0f, 1f));
        Vector3 positionB = playerTransform.position + new Vector3(randomInCircleB.x, 0, randomInCircleB.y) * receiverRadius.Evaluate(Random.Range(0f, 1f));
        Customer customerA = pool.AllocateObject<Customer>(0, positionA);
        customerA.gameObject.SetActive(true);
        Customer customerB = pool.AllocateObject<Customer>(0, positionB);
        customerB.gameObject.SetActive(true);
        PlayerClerk.PlayerClerkTask task = new PlayerClerk.PlayerClerkTask(customerA.gameObject, customerB.gameObject, 0);
        customerA.CurrentTask = task;
        customerB.CurrentTask = task;
    }

    private void PickNeighborCustomer()
    {
        
    }
}
