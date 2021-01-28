using System;
using System.Collections.Generic;
using UnityEngine;
using FuzeStudios.Variables;
using TMPro;

public class CustomerSpawner : Singleton<CustomerSpawner>, IFreezable
{
    public CustomerRuntimeSet Set;
    public FloatVariable SpawnCooldown;
    public IntegerVariable SpawnLimit;
    public List<CustomerProfile> CustomerProfilesList = new List<CustomerProfile>();

    public GameObject customerPrefab;
    public Transform customerSpawnPoint;

    private float cooldownTimer = 0.0f;
    public bool shouldSpawn = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (Set.Items.Count < SpawnLimit.Value)
        {
            if (shouldSpawn)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0.0f)
                {
                    Spawn();
                }
            }
        }
        else
        {
            Reset();
        }
    }

    public void Spawn()
    {
        Vector3 spawnPosition = new Vector3(
            customerSpawnPoint.position.x,
            customerSpawnPoint.position.y,
            customerSpawnPoint.position.z
        );

        GameObject newCustomer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);

        Customer customerScript = newCustomer.GetComponent<Customer>();
        int randomIndex = UnityEngine.Random.Range(0, CustomerProfilesList.Count);
        customerScript.profile = CustomerProfilesList[randomIndex];
        
        newCustomer.SetActive(true);
        cooldownTimer = SpawnCooldown.Value;
    }

    public void Reset()
    {
        cooldownTimer = 0;
    }

    public void Freeze()
    {
        shouldSpawn = false;
    }

    public void Defrost()
    {
        shouldSpawn = true;
    }
}
