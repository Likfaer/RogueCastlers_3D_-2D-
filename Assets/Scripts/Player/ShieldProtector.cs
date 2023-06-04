using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProtector : PlayerExist
{
    public GameObject shieldPrefab;
    public int maxObjects = 10; // Maximum number of objects to spawn
    public float orbitRadius = 5f; // Radius of the orbit
    public float orbitSpeed = 5f; // Speed of the orbit in degrees per second
    public float objectLifetime = 5f; // Lifetime of the spawned objects

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<float> targetAngles = new List<float>();
    private List<float> offsets = new List<float>();

    new private void Start()
    {
        SpawnObjectsInOrbit();
    }

    private void SpawnObjectsInOrbit()
    {
        int objectsToSpawn = Mathf.Min(maxObjects, 360 / (int)orbitSpeed);

        for (int i = 0; i < objectsToSpawn; i++)
        {
            float angle = i * (360f / objectsToSpawn) * Mathf.Deg2Rad;
            Vector3 spawnPosition = player.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * orbitRadius;

            GameObject shieldObject = Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);
            shieldObject.transform.SetParent(transform);
            shieldObject.GetComponent<RangeCollision>().damage = 2.5f;

            StartCoroutine(DestroyAfterLifetime(shieldObject, objectLifetime));

            spawnedObjects.Add(shieldObject);
            targetAngles.Add(angle);
            offsets.Add(Random.Range(0f, 360f));
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            GameObject spawnedObject = spawnedObjects[i];

            if (spawnedObject != null)
            {
                // Calculate the current angle based on time, object index, and offset
                float currentAngle = (Time.time * orbitSpeed + offsets[i]) * Mathf.Deg2Rad;

                // Calculate the target position on the orbit
                Vector3 targetPosition = player.transform.position + new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0f) * orbitRadius;

                // Move the object towards the target position
                spawnedObject.transform.position = Vector3.MoveTowards(spawnedObject.transform.position, targetPosition, orbitSpeed * Time.deltaTime);
            }
            else
            {
                RemoveObject(spawnedObject);
                i--; // Decrement the loop counter to account for the removed object
            }
        }

        if (spawnedObjects.Count < maxObjects)
        {
            SpawnObjectOnRight();
        }
    }


    private void SpawnObjectOnRight()
    {
        Vector3 spawnPosition = player.transform.position + new Vector3(0f, orbitRadius, 0f);

        GameObject shieldObject = Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);
        shieldObject.transform.SetParent(transform);
        shieldObject.GetComponent<RangeCollision>().damage = 2.5f;

        StartCoroutine(DestroyAfterLifetime(shieldObject, objectLifetime));

        spawnedObjects.Add(shieldObject);
        targetAngles.Add(0f);
        offsets.Add(Random.Range(0f, 360f));
    }



    private IEnumerator DestroyAfterLifetime(GameObject obj, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        if (obj != null)
        {
            RemoveObject(obj);
        }
    }

    public void RemoveObject(GameObject obj)
    {
        int index = spawnedObjects.IndexOf(obj);
        if (index >= 0)
        {
            spawnedObjects.RemoveAt(index);
            targetAngles.RemoveAt(index);
            offsets.RemoveAt(index);

            // Reset the target angle and offset for the removed object
            targetAngles.Insert(index, 0f);
            offsets.Insert(index, Random.Range(0f, 360f));
        }
        Destroy(obj);
    }
}
