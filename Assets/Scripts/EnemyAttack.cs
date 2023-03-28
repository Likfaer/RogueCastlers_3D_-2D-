using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    protected GameObject player;

    public virtual void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }
}
