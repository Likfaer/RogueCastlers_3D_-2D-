using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerInput : MonoBehaviour
{
    private Vector2 targetPos;
    private Vector2 direction;
    private Animator animator;
    private bool UpDown, LeftRight;

    public Text speedText;
    public Text dashText;

    private bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    private float dashTimeLeft;
    private float lastImagexpos;
    private float lastDash = -100f;

    public UnityEvent onStart, onDone;

    private enum Facing { UP, DOWN, LEFT, RIGHT };
    private Facing FacingDir = Facing.DOWN;

    public UnityEvent<Vector2> onMovementInput, onPointerInput;
    public UnityEvent onAttack;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;
    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }
    private void Start()
    {
        if (GameObject.Find("UI_Overlay"))
        {
            speedText = GameObject.Find("UI_Overlay/StatsPanel/Panel/SpeedText").GetComponent<Text>();
            dashText = GameObject.Find("UI_Overlay/StatsPanel/Panel/DashText").GetComponent<Text>();
            SetUI();
        }
    }
    private void Update()
    {
        onMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        onPointerInput?.Invoke(GetPointerInput());
        TakeInput();
        Move();
        CheckDash();

    }
    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void SetUI()
    {
        speedText.text = "Speed: " + gameObject.GetComponent<AgentMover>().maxSpeed.ToString();
        dashText.text = "Dash: " + dashSpeed.ToString();
    }
    private void Move()
    {
        //transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
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
            targetPos.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            FacingDir = Facing.LEFT;
            targetPos.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            FacingDir = Facing.DOWN;
            targetPos.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            FacingDir = Facing.RIGHT;
            targetPos.x = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                targetPos = Vector2.zero;
                if (GetComponent<Rigidbody2D>().velocity.x == 0 && GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    switch (FacingDir)
                    {
                        case Facing.UP:
                            //Debug.Log("UP");
                            targetPos.y = 1;
                            break;
                        case Facing.DOWN:
                            //Debug.Log("DOWN");
                            targetPos.y = -1;
                            break;
                        case Facing.LEFT:
                            //Debug.Log("LEFT");
                            targetPos.x = -1;
                            break;
                        case Facing.RIGHT:
                            //Debug.Log("RIGHT");
                            targetPos.x = 1;
                            break;
                        default:
                            break;
                    }
                }
                AttemptToDash();
            }
        }
    }
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
        PlayerAfterImagePool.instance.GetFromPool();
        lastImagexpos = transform.position.x;
        StartCoroutine(Reset(dashTime));
    }
    private void CheckDash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                //Debug.Log(targetPos.x + " : " + targetPos.y + " * " + dashSpeed);
                onStart?.Invoke();
                GetComponent<Rigidbody2D>().velocity = new Vector2(dashSpeed * targetPos.x, dashSpeed * targetPos.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImagexpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.instance.GetFromPool();
                    lastImagexpos = transform.position.x;
                }
            }
        }
    }
    private IEnumerator Reset(float value)
    {
        yield return new WaitForSeconds(value);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isDashing = false;
        onDone.Invoke();
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        //Debug.Log(direction);
        animator.SetLayerWeight(1, 1);
        if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
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
