using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : PlayerExist
{
    [SerializeField] private float smoothing;
    [SerializeField] private Vector3 offset;

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, player.transform.position + offset, smoothing);
            transform.position = newPosition;
        }
    }
}
