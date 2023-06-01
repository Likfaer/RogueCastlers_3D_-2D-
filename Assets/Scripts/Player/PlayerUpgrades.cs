using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   

public class PlayerUpgrades : PlayerExist
{
    public UnityEvent onPickUp;

    //NO DASH UPGRADES (NEED TO ADD?)

    //Refill Stats (on PlayerStats call)
    public void CurrentHealthChange(float value)
    {
        player.GetComponentInChildren<PlayerStats>().HealCharacter(value);
    }
    public void MaxHealthChange(float value)
    {
        player.GetComponentInChildren<PlayerStats>().MaxHPChange(value);
    }
    //Upgrades
    public void MeleeAttackDamageChange(float value)
    {
        player.GetComponentInChildren<WeaponParent>().minDamage += value;
        player.GetComponentInChildren<WeaponParent>().maxDamage += value;
        player.GetComponentInChildren<WeaponParent>().SetUI();
    }
    public void MeleeAttackSpeedChange(float value)
    {
        player.GetComponentInChildren<WeaponParent>().attackCooldown -= value;
        player.GetComponentInChildren<WeaponParent>().SetUI();
    }
    public void RangeAttackDamageChange(float value)
    {
        player.GetComponentInChildren<TestSpell>().minDamage += value;
        player.GetComponentInChildren<TestSpell>().maxDamage += value;
        player.GetComponentInChildren<TestSpell>().SetUI();
    }
    public void RangeAttackSpeedChange(float value)
    {
        player.GetComponentInChildren<TestSpell>().attackCooldown -= value;
        player.GetComponentInChildren<TestSpell>().SetUI();
    }
    public void SpeedChange(float value)
    {
        player.GetComponent<AgentMover>().maxSpeed += value;
        player.GetComponent<PlayerInput>().SetUI();
    }
}
