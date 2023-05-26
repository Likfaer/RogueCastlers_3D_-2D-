using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickUp : PlayerExist
{
    public enum pickupObject {COIN, GEM};
    public pickupObject currentObject;
    public int pickupQuantity;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerStats.playerStats.AddCurrency(this);
            Destroy(gameObject);
        }
    }
}
