using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if(collision.GetComponent<Enemy>() != null)
            {
                //Debug.Log(gameObject.layer);
                collision.GetComponent<Enemy>().DealDamage(damage, gameObject.layer);
                Destroy(gameObject);
            }
            if (collision.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
