using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingParent : PlayerExist
{
    public GameObject projectile;
    [SerializeField] private float minDamage, maxDamage, projectileForce;

    public override void Start()
    {
        base.Start();
    }
    public void Attack()
    {
        if (player != null)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector3 targetPos = player.transform.position;
            Vector3 myPos = transform.position;
            Vector3 direction = (targetPos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<EnemyRangeCollision>().damage = Random.Range(minDamage, maxDamage);
        }
    }
}
