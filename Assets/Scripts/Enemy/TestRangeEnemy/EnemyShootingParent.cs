using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingParent : EnemyAttack
{
    public GameObject projectile;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private float projectileForce;
    [SerializeField] private float cooldown;

    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if (player != null)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
           
            Vector3 targetPos = player.transform.position;
            Vector3 myPos = transform.position;
            Vector3 direction = (targetPos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<EnemyRangeCollision>().damage = Random.Range(minDamage, maxDamage);
            StartCoroutine(ShootPlayer());
        }
        
    }
}
