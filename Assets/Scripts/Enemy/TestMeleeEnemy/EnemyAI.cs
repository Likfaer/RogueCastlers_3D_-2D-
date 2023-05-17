using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : EnemyAttack
{
    [SerializeField] public float speed;

    [SerializeField] private float chaseDistanceThreshold = 200f; // если игрок в радиусе то бежим к нему
    [SerializeField] private float attackDistanceThreshold = 0.00001f; //если игрока можно ударить, то ударяем

    [SerializeField]
    private float attackDelay = 1;
    private float passTime = 1;

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 targetPos = player.transform.position;
            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);
            if (distance < chaseDistanceThreshold)
            {
                if (distance <= attackDistanceThreshold)
                {
                    //attack
                }
                else
                {
                    //chasing
                    Vector2 direction = player.transform.position - transform.position;
                    transform.Translate(direction * speed * Time.deltaTime);
                }
            }
            if (passTime < attackDelay)
            {
                passTime += Time.deltaTime;
            }
        }
    }
}
