using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerExist : MonoBehaviour
{
    protected GameObject player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
        if (player == null)
        {
            Debug.Log("find other");
            player = GameObject.FindWithTag("Player");
        }
    }
    public void SetDefaults()
    {
        //Base Activations
        player.GetComponentInChildren<Animator>().enabled = false;
        player.GetComponentInChildren<PlayerInput>().enabled = false;
        player.transform.Find("WeaponParent").gameObject.SetActive(false);
        player.transform.Find("RangeParent").gameObject.SetActive(false);

        //Movement Activations
        player.GetComponent<AgentMover>().maxSpeed = 0.65f;
        player.GetComponent<PlayerInput>().dashRange = 1f;

        //Damage Stats
        player.GetComponentInChildren<WeaponParent>().minDamage = 25f;
        player.GetComponentInChildren<WeaponParent>().minDamage = 50f;
        player.GetComponentInChildren<WeaponParent>().attackCooldown = 1f;
        player.GetComponentInChildren<WeaponParent>().radius = 0.075f;

        player.GetComponentInChildren<TestSpell>().minDamage = 20;
        player.GetComponentInChildren<TestSpell>().maxDamage = 30;
        player.GetComponentInChildren<TestSpell>().projectileForce = 2;
        player.GetComponentInChildren<TestSpell>().attackCooldown = 0.25f;

        //Player Stats

        player.GetComponentInChildren<PlayerStats>().health = 150;
        player.GetComponentInChildren<PlayerStats>().maxHealth = 150;

    }
    void OnApplicationQuit()
    {
        //SetDefaults();
    }
    public virtual void Start()
    {
        
    }
}
