using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public CustomerSpawner spawner;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (spawner.shouldSpawn)
        {
            source.enabled = true;
        }
        else
        {
            source.enabled = false;
        }
    }
}
