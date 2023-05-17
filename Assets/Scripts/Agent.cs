using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MiscUtil.Threading;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{

    //Sunny Valley Movement

    private Vector2 targetPos;

    private AgentMover agentMover;

    private Vector2 pointerInput, movementInput;
    
    private WeaponParent weaponParent;
    private EnemyWeaponParent EnemyWeaponParent;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    // Sunny Valley Movement break
    private void Awake()
    {
        
        if (LayerMask.LayerToName(gameObject.layer) == "Player")
        {
            weaponParent = GetComponentInChildren<WeaponParent>();
        }
        else
        {
            EnemyWeaponParent = GetComponentInChildren<EnemyWeaponParent>();
        }
        
        agentMover = GetComponent<AgentMover>();
    }

    void Update()
    {
        agentMover.MovementInput = MovementInput;
    }

    public void PerformAttack()
    {
        if (LayerMask.LayerToName(gameObject.layer) == "Player")
        {
            weaponParent.Attack();
        }
        else
        {
            EnemyWeaponParent.Attack();
        }
    }
    
}
