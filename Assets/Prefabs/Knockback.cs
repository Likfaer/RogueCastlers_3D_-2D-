using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    float strength = 1, delay = 0.15f;

    public UnityEvent onBegin, onDone;

    public void PlayFeedback(GameObject sender)
    {
        StopCoroutine(Reset());
        onBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        //Debug.Log(direction);
        rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        onDone.Invoke();

    }
}
