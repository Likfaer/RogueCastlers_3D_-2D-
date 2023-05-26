using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToPlayer : PlayerExist
{
    public float speed;
    private float range;


     void Update()
    {
        if (player != null)
        {
            range = Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2f) + Mathf.Pow(transform.position.y - player.transform.position.y, 2f));
            if (range < 0.40f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
        
    }
}
