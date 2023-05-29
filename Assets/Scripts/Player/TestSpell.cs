using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] public float minDamage;
    [SerializeField] public float maxDamage;
    [SerializeField] public float projectileForce;

    [SerializeField] public float attackCooldown;
    [SerializeField] public float lastAttackTime;

    private Text AttackText;
    private Text AttackSpeedText;

    private void Start()
    {
        if (GameObject.Find("UI_Overlay"))
        {
            AttackText = GameObject.Find("UI_Overlay/StatsPanel/Panel/RAtkDmgText").GetComponent<Text>();
            AttackSpeedText = GameObject.Find("UI_Overlay/StatsPanel/Panel/RAtkSpeedText").GetComponent<Text>();
            SetUI();
        }
    }
    public void SetUI()
    {
        AttackText.text = "RAtkDmg: " + minDamage + " - " + maxDamage;
        AttackSpeedText.text = "RAtkS: " + attackCooldown;
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
            spell.GetComponent<RangeCollision>().damage = Random.Range(minDamage,maxDamage);
        }
    }
}
