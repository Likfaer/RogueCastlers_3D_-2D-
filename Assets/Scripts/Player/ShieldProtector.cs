using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProtector : PlayerExist
{
    public GameObject shieldPrefab;

    public int maxObjects = 1; 
    public float orbitRadius = 0.25f; 
    public float orbitSpeed = 30f;
    public float damage = 2f;
    
    private float spawnDelay = 0.5f;

    //public float objectLifetime = 5f;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private List<float> targetAngles = new List<float>();

    new void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnObject();
        StartCoroutine(SpawnObjects());
    }

/*    private IEnumerator DestroyAfterLifetime(GameObject obj)
    {
        yield return new WaitForSeconds(objectLifetime);
        if (obj != null)
        {
            RemoveObject(obj);
        }
    }*/

    private void SpawnObject()
    {
        if (spawnedObjects.Count < maxObjects)
        {
            Vector3 spawnPosition = player.transform.position + new Vector3(orbitRadius, 0f, 0f);
            GameObject shieldObject = Instantiate(shieldPrefab, spawnPosition, Quaternion.identity);

            shieldObject.transform.SetParent(transform);
            shieldObject.GetComponent<RangeCollision>().damage = damage;

            shieldObject.GetComponent<RangeCollision>().destroyable = false;

            //StartCoroutine(DestroyAfterLifetime(shieldObject));

            spawnedObjects.Add(shieldObject);
            targetAngles.Add(0f);
        }
    }
    private void Update()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            GameObject spawnedObject = spawnedObjects[i];

            if (spawnedObject != null)
            {
                float currentAngle = (Time.time * orbitSpeed  - (i * 360 / spawnedObjects.Count)) * Mathf.Deg2Rad;

                targetAngles[i] = currentAngle;
                Vector3 targetPosition = player.transform.position + new Vector3(Mathf.Cos(targetAngles[i]), Mathf.Sin(targetAngles[i])) * orbitRadius;
                spawnedObject.transform.position = Vector3.MoveTowards(spawnedObject.transform.position, targetPosition, orbitSpeed * Time.deltaTime);

            }
        }
        //Debug.Log("obj =" + spawnedObjects.Count + " | angles = " + targetAngles.Count + " | offsets = " + offsets.Count + " | spawnDelay = " + spawnDelay + " | Time = " + Time.time);
    }


    public void RemoveObject(GameObject obj)
    {
        int index = spawnedObjects.IndexOf(obj);
        if (index >= 0)
        {
            spawnedObjects.RemoveAt(index);
            targetAngles[index] = 0f;
        }
        Destroy(obj);
    }
}
