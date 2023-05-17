using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, onAttackPeformed;
    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }
    public void TriggerAttack()
    {
        onAttackPeformed?.Invoke();
    }
}
