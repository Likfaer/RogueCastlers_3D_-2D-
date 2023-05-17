using Ludiq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector3 MousePos { get; set; }

    public float minDamage;
    public float maxDamage;
    public float attackCooldown;
    private float lastAttackTime;
    public Animator animator;

    private bool attackBlocked;

    public Transform circleOrigin;
    public float radius;

    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = 2f;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseposition);
        
        Vector2 direction = (mousePos - (Vector3)transform.position).normalized;

        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        } else if(direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach( Collider2D collision in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Debug.Log(collision.name);
            if (collision.name != "Player")
            {
                if (collision.GetComponent<Enemy>() != null)
                {
                    collision.GetComponent<Enemy>().DealDamage(UnityEngine.Random.Range(minDamage, maxDamage));
                }
                if (collision.GetComponent<TestEnemyProjectile>() != null)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
