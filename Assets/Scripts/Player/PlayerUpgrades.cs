using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YamlDotNet.Core.Tokens;

public class PlayerUpgrades : PlayerExist
{
    [SerializeField] public float value;
    public UnityEvent onPickUp;

    //NO DASH UPGRADES (NEED TO ADD?)

    //Upgrades
    public void MeleeAttackDamageUpgrade(float value)
    {
        player.GetComponentInChildren<WeaponParent>().minDamage += value;
        player.GetComponentInChildren<WeaponParent>().maxDamage += value;
        player.GetComponentInChildren<WeaponParent>().SetUI();
    }
    public void MeleeAttackSpeedUpgrade(float value)
    {
        player.GetComponentInChildren<WeaponParent>().attackCooldown -= value;
        player.GetComponentInChildren<WeaponParent>().SetUI();
    }
    public void RangeAttackDamageUpgrade(float value)
    {
        player.GetComponentInChildren<TestSpell>().minDamage += value;
        player.GetComponentInChildren<TestSpell>().maxDamage += value;
        player.GetComponentInChildren<TestSpell>().SetUI();
    }
    public void RangeAttackSpeedUpgrade(float value)
    {
        player.GetComponentInChildren<TestSpell>().attackCooldown -= value;
        player.GetComponentInChildren<TestSpell>().SetUI();
    }
    public void SpeedUpgrade(float value)
    {
        player.GetComponent<AgentMover>().maxSpeed += value;
        player.GetComponent<PlayerInput>().SetUI();
    }

    //Degrades
    public void MeleeAttackDamageReduce(float value)
    {
        player.GetComponentInChildren<WeaponParent>().minDamage -= value;
        player.GetComponentInChildren<WeaponParent>().maxDamage -= value;
        player.GetComponentInChildren<WeaponParent>().SetUI();
    }
    public void MeleeAttackSpeedReduce(float value)
    {
        player.GetComponentInChildren<WeaponParent>().attackCooldown += value;
        player.GetComponentInChildren<WeaponParent>().SetUI();
    }
    public void RangeAttackDamageReduce(float value)
    {
        player.GetComponentInChildren<TestSpell>().minDamage -= value;
        player.GetComponentInChildren<TestSpell>().maxDamage -= value;
        player.GetComponentInChildren<TestSpell>().SetUI();
    }
    public void RangeAttackSpeedReduce(float value)
    {
        player.GetComponentInChildren<TestSpell>().attackCooldown += value;
        player.GetComponentInChildren<TestSpell>().SetUI();
    }
    public void SpeedReduce(float value)
    {
        player.GetComponent<AgentMover>().maxSpeed -= value;
        player.GetComponent<PlayerInput>().SetUI();
    }
}
