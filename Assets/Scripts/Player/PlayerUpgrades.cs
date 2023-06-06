using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUpgrades : PlayerExist
{
    public UnityEvent onPickUp, onEnterTrigger;
    private Text UpgradeValueText;

    new private void Start()
    {
        UpgradeValueText = GameObject.Find("UI_Overlay").GetComponent<OverlayUI>().UpgradeValueText;
    }
    //NO DASH UPGRADES (NEED TO ADD?)

    //Weapons
    public void MeleeActive()
    {
        if (player.transform.Find("WeaponParent").gameObject.activeSelf == false)
        {
            player.transform.Find("WeaponParent").gameObject.SetActive(true);
        }
        else
        {
            player.GetComponentInChildren<WeaponParent>().minDamage += 5;
            player.GetComponentInChildren<WeaponParent>().maxDamage += 5;
            player.GetComponentInChildren<WeaponParent>().SetUI();
        }
        
    }
    public void SpellActive()
    {
        if (player.transform.Find("RangeParent/TestSpell").gameObject.activeSelf == false)
        {
            player.transform.Find("RangeParent/TestSpell").gameObject.SetActive(true);
        }
        else
        {
            player.GetComponentInChildren<TestSpell>().minDamage += 5;
            player.GetComponentInChildren<TestSpell>().maxDamage += 5;
            player.GetComponentInChildren<TestSpell>().SetUI();
        }
        if (player.transform.Find("RangeParent/BounceSpell").gameObject.activeSelf == true && player.transform.Find("RangeParent/TestSpell").gameObject.activeSelf == true)
        {
            player.transform.Find("RangeParent").GetComponent<WeaponSwitcher>().enabled = true;
        }
    }
    public void BounceActive()
    {
        if (player.transform.Find("RangeParent/BounceSpell").gameObject.activeSelf == false)
        {
            player.transform.Find("RangeParent/BounceSpell").gameObject.SetActive(true);
        }
        else
        {
            player.GetComponentInChildren<TestSpell>().minDamage += 5;
            player.GetComponentInChildren<TestSpell>().maxDamage += 5;
            player.GetComponentInChildren<TestSpell>().SetUI();
        }
        if (player.transform.Find("RangeParent/BounceSpell").gameObject.activeSelf == true && player.transform.Find("RangeParent/TestSpell").gameObject.activeSelf == true)
        {
            player.transform.Find("RangeParent").GetComponent<WeaponSwitcher>().enabled = true;
        }
    }
    public void ShieldActive()
    {
        if (player.transform.Find("ShieldProtector").gameObject.activeSelf == false)
        {
            player.transform.Find("ShieldProtector").gameObject.SetActive(true);
        }
        else
        {
            player.GetComponentInChildren<ShieldProtector>().maxObjects += 1;
        }

    }

    //Refill Stats (on PlayerStats call)
    public void SetUI(float value)
    {
        UpgradeValueText.text = value.ToString();
    }
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
