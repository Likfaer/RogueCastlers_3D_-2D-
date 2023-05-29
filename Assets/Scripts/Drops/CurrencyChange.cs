using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyChange : PlayerExist
{
    public enum pickupObject {COIN, GEM};
    public pickupObject currentObject;
    public int pickupQuantity;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (this.currentObject == pickupObject.COIN)
            {
               //Debug.Log(PlayerStats.playerStats.coins + " + " + pickupQuantity + " > 0 ?");
               if (PlayerStats.playerStats.coins + pickupQuantity >= 0)
                {
                    if (gameObject.GetComponent<PlayerUpgrades>() != null)
                    {
                        // Это хилка, надо придумать как различать хилящие шмотки от бафающих
                        //Debug.Log(gameObject.GetComponent<PlayerUpgrades>().value);
                        //PlayerStats.playerStats.HealCharacter(gameObject.GetComponent<PlayerUpgrades>().value);

                        gameObject.GetComponent<PlayerUpgrades>().onPickUp?.Invoke();
                    }
                    PlayerStats.playerStats.AddCurrency(this);
                    Destroy(gameObject);
                }
            }
            if (this.currentObject == pickupObject.GEM)
            {
                if (PlayerStats.playerStats.gems + pickupQuantity >= 0)
                {
                    
                    if (gameObject.GetComponent<PlayerUpgrades>() != null)
                    {
                        gameObject.GetComponent<PlayerUpgrades>().onPickUp?.Invoke();
                    }
                    PlayerStats.playerStats.AddCurrency(this);
                    Destroy(gameObject);
                }
            }
        }
    }
}
