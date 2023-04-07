using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;

    public float attackCooldown;
    private float lastAttackTime;

    public Text AttackText;
    public Text AttackSpeedText;

    private void Start()
    {
        SetAttackUI();
        SetAttackSpeedUI();
    }
    private void SetAttackUI()
    {
        AttackText.text = "Atk: " + minDamage + " - " + maxDamage;
    }
    private void SetAttackSpeedUI()
    {
        AttackSpeedText.text = "AtkS: " + attackCooldown;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector3 mouseposition = Input.mousePosition;
            mouseposition.z = 2f;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseposition);

            Vector3 myPos = transform.position;
            Vector3 dir = (mousePos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = dir * projectileForce;
            spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage,maxDamage);
        }
    }
}
