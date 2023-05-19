using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExist : MonoBehaviour
{
    protected GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
    }
    public virtual void Start()
    {
        
    }
    public void FixedUpdate()
    {
    }
}
