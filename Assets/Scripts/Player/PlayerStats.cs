using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public GameObject player;

    public float health;
    public float maxHealth;

    public Text healthText;
    public Slider healthSlider;
    
    public int coins;
    public int gems;

    public Text coinsValue;
    public Text gemsValue;


    private void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        //DontDestroyOnLoad(this);
        healthText = GameObject.Find("UI_Overlay/UserPanel/HealthText").GetComponent<Text>();
        healthSlider = GameObject.Find("UI_Overlay/UserPanel/HealthSlider").GetComponent<Slider>();
        coinsValue = GameObject.Find("UI_Overlay/UserPanel/CoinsValue").GetComponent<Text>();
        gemsValue = GameObject.Find("UI_Overlay/UserPanel/CoinsValue").GetComponent<Text>();
    }
    void Start()
    {
        health = maxHealth;
        SetHealthUI();
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
        SetHealthUI();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
    }
    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
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
            health = 0;
            Destroy(player);
            PlayerPrefs.SetInt("RoomsCount", 0);
        }
    }
    float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    public void AddCurrency(CurrencyPickUp currency)
    {
        if (currency.currentObject == CurrencyPickUp.pickupObject.COIN)
        {
            coins += currency.pickupQuantity;
            coinsValue.text = "Coins:" + coins.ToString();

        }
        else if (currency.currentObject == CurrencyPickUp.pickupObject.GEM)
        {
            gems += currency.pickupQuantity;
            gemsValue.text = "Gems:" + gems.ToString();
        }
    }

}
