using Ludiq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemySpawner : PlayerExist
{
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private float SpawnRate;

    GameObject EnemiesList;
    public int countEnemiesNow;
    private bool isCoroutineRunning;
    private float x, y;
    private Vector3 spawnPos;
    private float defaultX, defaultY;

    public void StartSpawn(int numEnemies, float DefX, float DefY)
    {
        EnemiesList = GameObject.Find("EnemiesList");
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
                //Debug.Log("Spawned enemy with id : " + rand);
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
}
