using Ludiq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public float SpawnRate;
    public GameObject player;

    GameObject EnemiesList;
    public int countEnemiesNow;
    private bool isCoroutineRunning;
    private float x, y;
    private Vector3 spawnPos;
    private float defaultX, defaultY;
    void Start()
    {
        
    }
    public void StartSpawn(int numEnemies, float DefX, float DefY)
    {
        EnemiesList = GameObject.Find("EnemiesList");
        player = GameObject.Find("Player");
        defaultX = DefX;
        defaultY = DefY;
        //Debug.Log("enemies in StartSpawn: " + numEnemies);
        StartCoroutine(SpawnTestEnemy(numEnemies));
    }
    IEnumerator SpawnTestEnemy(int numEnemies)
    {
        //Debug.Log("enemies in SpawnTestEnemy: " + numEnemies);
        isCoroutineRunning = true;
        while (numEnemies > 0)
        {
            //Debug.Log("Total will be spawned: " + numEnemies);
            x = Random.Range(-0.225f, 0.225f);
            y = Random.Range(-0.225f, 0.225f);
            spawnPos.x = (Random.Range(0.3f, defaultX / 2f - 0.3f) + x);
            spawnPos.y = (Random.Range(0.3f, defaultY / 2f - 0.3f) + y);
            if (player != null)
            {
                int rand = Random.Range(0, Enemies.Count);
                Debug.Log(rand);
                GameObject Enemy = Instantiate(Enemies[rand], spawnPos, Quaternion.identity);
                Enemy.transform.parent = EnemiesList.transform;
            }
            yield return new WaitForSeconds(SpawnRate);
            numEnemies--;
        }
        isCoroutineRunning = false;
    }
    public int getEnemiesNow()
    {
        return countEnemiesNow = EnemiesList.transform.childCount;
    }
    public bool IsCoroutineRunning()
    {
        return isCoroutineRunning;
    }
    void Update()
    {
        player = GameObject.Find("Player");
         
    }
}
