using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float minStrength = 0.5f, Maxstrength = 1, delay = 0.15f;

    public UnityEvent onBegin, onDone;

    public void PlayFeedback(GameObject sender)
    {
        StopCoroutine(Reset());
        onBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        //Debug.Log(direction);
        
        rb2d.AddForce(direction * Random.Range(minStrength, Maxstrength), ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        onDone.Invoke();

    }
}
