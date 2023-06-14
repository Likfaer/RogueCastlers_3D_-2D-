using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;

public class EnemyAI : PlayerExist
{
    public UnityEvent<Vector2> onMovementInput, onPointerInput;
    public UnityEvent onAttack;

    /*[SerializeField] private float speed;*/

    [SerializeField] private float chaseDistance = 1.5f; // если игрок в радиусе то бежим к нему
    [SerializeField] public float attackDistance = 0.25f; //если игрока можно ударить, то ударяем

    [SerializeField] private float attackCooldown;
    [SerializeField] public bool ranged;
    private float lastAttackTime;

    private bool playerLoss = true;

    // animation
    private Animator animator;
    private Vector2 direction;
    private bool UpDown, LeftRight;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 targetPos = player.transform.position;
            float distance = Vector2.Distance(targetPos, gameObject.transform.position);
            if (distance < chaseDistance)
            {
                playerLoss = false;
                if (distance <= attackDistance)
                {
                    animator.SetLayerWeight(1, 0);
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
                    if (playerLoss == false)
                    {
                        animator.SetBool("Stay", false);
                        direction = targetPos - transform.position;
                        SetAnimatorMovement(direction);
                        //Debug.Log(direction);
                        onMovementInput?.Invoke(direction.normalized);
                    }
                }
            }
            else
            {
                playerLoss = true;
                onMovementInput?.Invoke(Vector2.zero);
                animator.SetBool("Stay", true);
            }
        }
        
    }
    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetFloat("xDir", direction.x);
            animator.SetFloat("yDir", 0);
        }
        else
        {
            animator.SetFloat("xDir", 0);
            animator.SetFloat("yDir", direction.y);
        }
        animator.SetBool("LeftRight", LeftRight);
        animator.SetBool("UpDown", UpDown);
    }

}
