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
        isCoroutineRunning = true;
        //Debug.Log("enemies in SpawnTestEnemy: " + numEnemies);
        while (numEnemies > 1)
        {
            //Debug.Log("Total will be spawned: " + numEnemies);
            spawnPos.x = (Random.Range(0.3f, defaultX / 2f - 0.3f) + Random.Range(-0.225f, 0.225f));
            spawnPos.y = (Random.Range(0.3f, defaultY / 2f - 0.3f) + Random.Range(-0.225f, 0.225f));
            if (player != null)
            {
                //Debug.Log("Spawned enemy with id : " + rand);
                GameObject Enemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)], spawnPos, Quaternion.identity);
                Enemy.transform.parent = EnemiesList.transform;
            }
            yield return new WaitForSeconds(SpawnRate);
            numEnemies--;
        }
        yield return new WaitForSeconds(SpawnRate * 2.5f);
        //Debug.Log("big boy spawned");
        spawnPos.x = (Random.Range(0.3f, defaultX / 2f - 0.3f) + Random.Range(-0.225f, 0.225f));
        spawnPos.y = (Random.Range(0.3f, defaultY / 2f - 0.3f) + Random.Range(-0.225f, 0.225f));
        if (player != null)
        {
            //Debug.Log("Spawned enemy with id : " + rand);
            GameObject Enemy = Instantiate(Enemies[Random.Range(0, Enemies.Count)], spawnPos, Quaternion.identity);
            Enemy.transform.parent = EnemiesList.transform;
            Enemy.transform.localScale = new Vector3(1.25f, 1.25f, 1f);
            Enemy.GetComponent<Enemy>().maxHealth *= 1.5f;
            Enemy.GetComponent<Enemy>().health *= 1.5f;
            Enemy.GetComponent<Knockback>().enabled = false;
            Enemy.GetComponent<EnemyAI>().attackDistance += 0.02f;
            if (Enemy.GetComponent<EnemyAI>().ranged == true)
            {
                Enemy.GetComponentInChildren<EnemyShootingParent>().minDamage *= 1.5f;
                Enemy.GetComponentInChildren<EnemyShootingParent>().maxDamage *= 1.5f;
                Enemy.GetComponentInChildren<EnemyShootingParent>().projectileForce *= 1.5f;
            }
            else
            {
                Enemy.GetComponentInChildren<EnemyWeaponParent>().minDamage *= 1.5f;
                Enemy.GetComponentInChildren<EnemyWeaponParent>().maxDamage *= 1.5f;
            }
        }
        numEnemies--;
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
