using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        
    }

    //Upgrades
    public void MeleeAttackDamageUpgrade(float value)
    {
        player.GetComponentInChildren<WeaponParent>().minDamage += value;
        player.GetComponentInChildren<WeaponParent>().maxDamage += value;
    }
    public void MeleeAttackSpeedUpgrade(float value)
    {
        player.GetComponentInChildren<WeaponParent>().attackCooldown -= value;
    }
    public void RangeAttackDamageUpgrade(float value)
    {
        player.GetComponent<TestSpell>().minDamage += value;
        player.GetComponent<TestSpell>().maxDamage += value;
    }
    public void RangeAttackSpeedUpgrade(float value)
    {
        player.GetComponent<TestSpell>().attackCooldown -= value;
    }
    public void SpeedUpgrade(float value)
    {
        player.GetComponent<PlayerInput>().speed += value;
    }

    //Degrades
    public void MeleeAttackDamageReduce(float value)
    {
        player.GetComponentInChildren<WeaponParent>().minDamage -= value;
        player.GetComponentInChildren<WeaponParent>().maxDamage -= value;
    }
    public void MeleeAttackSpeedReduce(float value)
    {
        player.GetComponentInChildren<WeaponParent>().attackCooldown += value;
    }
    public void RangeAttackDamageReduce(float value)
    {
        player.GetComponent<TestSpell>().minDamage -= value;
        player.GetComponent<TestSpell>().maxDamage -= value;
    }
    public void RangeAttackSpeedReduce(float value)
    {
        player.GetComponent<TestSpell>().attackCooldown += value;
    }
    public void SpeedReduce(float value)
    {
        player.GetComponent<PlayerInput>().speed -= value;
    }
}
