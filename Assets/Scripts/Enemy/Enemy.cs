using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBar;
    public Slider healthBarSlider;

    [SerializeField] private GameObject lootDrop;
    [SerializeField] private int lootValue;

    void Start()
    {
        IncreasePerRoom();
        health = maxHealth;
    }
    public void IncreasePerRoom()
    {
        int hardExpand = PlayerPrefs.GetInt("RoomsCount");
        maxHealth = maxHealth * (1 + hardExpand * 0.02f);
        Debug.Log("maxHealthNow" + maxHealth);
    }
    public void DealDamage(float damage, GameObject sender)
    {
        if (sender.layer == gameObject.layer)
            return;
        healthBar.SetActive(true);
        health -= damage;

        if (sender.name == "WeaponParent")
        {
            //Debug.Log("calling knockback");
            gameObject.GetComponent<Knockback>().PlayFeedback(sender);
        }

        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }
    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }
    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }
    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject loot = Instantiate(lootDrop, transform.position,Quaternion.identity);
            loot.GetComponent<CurrencyChange>().pickupQuantity = lootValue;
            loot.transform.parent = GameObject.Find("DropList").transform;
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }
}
