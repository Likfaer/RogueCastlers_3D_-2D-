using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MiscUtil.Threading;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 targetPos;
    public float speed;
   
    private Vector2 direction;
    private Animator animator;
    private bool UpDown, LeftRight;

    public Text speedText;
    public Text dashText;

    public float dashRange;

    private enum Facing {UP, DOWN, LEFT, RIGHT};
    private Facing FacingDir = Facing.DOWN;

    private WeaponParent weaponParent;

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        getSpeedUI();
        getDashUI();
    }

    void Update()
    {
        weaponParent.MousePos = targetPos;
        TakeInput();
        Move();
    }
    private void getSpeedUI()
    {
        speedText.text = "Speed: " + speed.ToString();
    }
    private void getDashUI()
    {
        dashText.text = "Dash: " + dashRange.ToString();
    }
    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x !=0 || direction.y !=0)
        {
            SetAnimatorMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
        
    }
    private void TakeInput()
    {
        direction = Vector2.zero; // 0,0,0

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            FacingDir = Facing.UP;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            FacingDir = Facing.LEFT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            FacingDir = Facing.DOWN;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            FacingDir = Facing.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 currentPos = transform.position;
            targetPos = Vector2.zero;
            if (FacingDir == Facing.UP)
            {
                targetPos.y = 1;
                UpDown = true;
                LeftRight = false;
            }
            else if (FacingDir == Facing.DOWN)
            {
                targetPos.y = -1;
                UpDown = false;
                LeftRight = false;
            }
            else if (FacingDir == Facing.LEFT)
            {
                targetPos.x = -1;
                UpDown = false;
                LeftRight = false;
            }
            else if (FacingDir == Facing.RIGHT)
            {
                targetPos.x = 1;
                UpDown = false;
                LeftRight = true;
            }

            Vector2 dashVector = targetPos * dashRange;

            int playerLayer = LayerMask.NameToLayer("Player");
            int layerMask = ~(1 << playerLayer); // where choose tags there layers

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dashVector.normalized, dashVector.magnitude, layerMask);

            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                Debug.Log(dashVector);
                dashVector = targetPos * (dashRange * 0.05f);
            }

            // Perform the dash
            transform.Translate(dashVector);


        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
        animator.SetBool("LeftRight", LeftRight);
        animator.SetBool("UpDown", UpDown);
    }
}
