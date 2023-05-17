using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : EnemyAttack
{
    public UnityEvent<Vector2> onMovementInput, onPointerInput;
    public UnityEvent onAttack;

    /*[SerializeField] private float speed;*/

    [SerializeField] private float chaseDistance = 1.5f; // если игрок в радиусе то бежим к нему
    [SerializeField] private float attackDistance = 0.25f; //если игрока можно ударить, то ударяем

    [SerializeField] private float attackCooldown;
    private float lastAttackTime;

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
            if (distance < chaseDistance)
            {
                if (distance <= attackDistance)
                {
                    //attack
                    onMovementInput?.Invoke(Vector2.zero);
                    if (Time.time - lastAttackTime > attackCooldown)
                    {
                        lastAttackTime = Time.time;
                        onAttack?.Invoke();
                    }
                }
                else
                {
                    //chasing
                    Vector2 direction = player.transform.position - transform.position;
                    onMovementInput?.Invoke(direction.normalized);
                }
            }
        }
    }
}
