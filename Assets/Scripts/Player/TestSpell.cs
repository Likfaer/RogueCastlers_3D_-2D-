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

    public Text AttackText;
    private void Start()
    {
        SetAttackUI();
    }
    private void SetAttackUI()
    {
        AttackText.text = "Atk: " + minDamage + " - " + maxDamage;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
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
