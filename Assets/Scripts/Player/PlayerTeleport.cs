using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public GameObject player;
    Transform playerPos;

    void Start()
    {
        playerPos = player.transform;
    }
    void NewPosition(float x,float y, float z)
    {
        playerPos.position = new Vector3(x, y, z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
