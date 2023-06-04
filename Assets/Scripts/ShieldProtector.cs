using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProtector : PlayerExist
{
    public GameObject shieldPrefab;
    public float spawnRate = 1f; // Rate at which objects are spawned per second
    public float spawnRadius = 5f; // Radius within which objects should be spawned
    public float lifetime = 5f; // Lifetime of the spawned objects


    new private void Start()
    {
        player.transform.position = transform.position;

        // Start the spawning coroutine
        StartCoroutine(SpawnObjects());
    }

    private System.Collections.IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Spawn object in a circular pattern
            float angle = Random.Range(0f, 2f * Mathf.PI); // Randomly select an angle

            Vector3 spawnPosition = player.transform.position + new Vector3(Mathf.Cos(angle), 0f,  0f) * spawnRadius; // Calculate spawn position

            GameObject spawnedObject = Instantiate(shieldPrefab, spawnPosition, Quaternion.identity); // Instantiate the object

            // Set the parent of the spawned object to the parent object
            spawnedObject.transform.parent = player.transform;

            // Destroy the object after the specified lifetime
            Destroy(spawnedObject, lifetime);

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void LateUpdate()
    {
        // Track parent GameObject's position
        transform.position = player.transform.position;
    }
}


