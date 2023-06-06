using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCollision : MonoBehaviour
{
    public float damage = 0;
    public bool destroyable = true;
    public bool reflexible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if(collision.GetComponent<Enemy>() != null)
            {
                //Debug.Log(gameObject.layer);
                collision.GetComponent<Enemy>().DealDamage(damage, gameObject);
                if (destroyable)
                {
                    Destroy(gameObject);
                }
            }
            else if (collision.CompareTag("Wall"))
            {
                if (reflexible)
                {
                    Vector2 wallNormal = collision.transform.up;
                    Vector2 incomingDirection = GetComponent<Rigidbody2D>().velocity.normalized;
                    Vector2 reflectionDirection = Vector2.Reflect(incomingDirection, wallNormal);
                    GetComponent<Rigidbody2D>().velocity = reflectionDirection * GetComponent<Rigidbody2D>().velocity.magnitude;

                    float angle = Mathf.Atan2(reflectionDirection.y, reflectionDirection.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else if (destroyable)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
