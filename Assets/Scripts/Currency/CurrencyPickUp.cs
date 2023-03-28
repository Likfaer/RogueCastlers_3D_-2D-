using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickUp : MonoBehaviour
{
    public enum pickupObject {COIN, GEM};
    public pickupObject currentObject;
    public int pickupQuantity;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerStats.playerStats.AddCurrency(this);
            Destroy(gameObject);
        }
    }
}
