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

    public float damageCooldown = 0.45f; 
    private bool canTakeDamage = true;
    private Coroutine damageFlashCoroutine;

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
        if (canTakeDamage)
        {
            health -= damage;
            CheckDeath();
            SetHealthUI();
            // Trigger damage flash effect
            if (damageFlashCoroutine != null)
            {
                StopCoroutine(damageFlashCoroutine);
            }
            damageFlashCoroutine = StartCoroutine(FlashDamageEffect());
            canTakeDamage = false;
            StartCoroutine(EnableDamageAfterCooldown());
        }
    }
    IEnumerator FlashDamageEffect()
    {
        Color flashColor = Color.red;
        flashColor.a = 0.5f; // Set the desired alpha value for the flashing effect
        Color originColor = player.transform.GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 2; i++)
        {
            player.transform.GetComponent<SpriteRenderer>().color = flashColor;
            yield return new WaitForSeconds(0.1f); 
            player.transform.GetComponent<SpriteRenderer>().color = originColor;
            yield return new WaitForSeconds(0.1f); 
        }
        player.transform.GetComponent<SpriteRenderer>().color = originColor; 
    }
    IEnumerator EnableDamageAfterCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
    }
    public void MaxHPChange(float value)
    {
        maxHealth += value;
        CheckOverheal();
        SetHealthUI();
    }
    public void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }
    public void CheckOverheal()
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

    public void AddCurrency(CurrencyChange currency)
    {
        if (currency.currentObject == CurrencyChange.pickupObject.COIN)
        {
            coins += currency.pickupQuantity;
            coinsValue.text = "Coins:" + coins.ToString();
        }
        else if (currency.currentObject == CurrencyChange.pickupObject.GEM)
        {
            gems += currency.pickupQuantity;
            gemsValue.text = "Gems:" + gems.ToString();
        }
    }

}
