using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeCollision : MonoBehaviour
{
    public float damage = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerStats.playerStats.DealDamage(damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
