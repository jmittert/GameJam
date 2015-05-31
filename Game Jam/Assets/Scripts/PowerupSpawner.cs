﻿using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour {

    float randomSpawnTime;
    float age = 0f;
    int pickupToSpawn;
    bool powerupOnLevel = false;
    public float earliestSpawn = 0f;
    public float latestSpawn = 25f;
    public bool spawnLightEmUp = true;
    public bool spawnMoreArrows = true;
    public bool spawnBetterBow = true;

    public GameObject LightEmUp;
    public GameObject MoreArrows;
    public GameObject BetterBow;

    // Use this for initialization
    void Start () {
        Setup();
    }

    void Setup()
    {
        if (!spawnLightEmUp && !spawnMoreArrows && !spawnBetterBow)
        {
            Destroy(gameObject);
            return;
        }
        randomSpawnTime = Random.Range(earliestSpawn, latestSpawn);
        pickupToSpawn = Random.Range(0, 3);
        while ((!spawnLightEmUp && pickupToSpawn == 0) || (!spawnMoreArrows && pickupToSpawn == 1) || (!BetterBow && pickupToSpawn == 2))
        {
            pickupToSpawn = Random.Range(0, 2);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!powerupOnLevel)
        {
            age += Time.deltaTime;
        }
        
        if (age >= randomSpawnTime)
        {
            foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
            {
                spawner.SendMessage("PickupSpawned");
            }
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject powerUp;
        switch (pickupToSpawn)
        {
            case 0:
                powerUp = (GameObject)Instantiate(LightEmUp, transform.position, transform.rotation);
                break;
            case 1:
                powerUp = (GameObject)Instantiate(MoreArrows, transform.position, transform.rotation);
                break;
            case 2:
                powerUp = (GameObject)Instantiate(BetterBow, transform.position, transform.rotation);
                break;
        }
    }

    void Reset()
    {
        powerupOnLevel = false;
        Setup();
    }

    void PickupSpawned()
    {
        powerupOnLevel = true;
        age = 0;
    }
}