using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : UnityEvent<string>
{

}
public class WeaponAnimationEvents : MonoBehaviour
{
    public void OnAnimationEvent(string eventName)
    {
        //WeaponAnimationEvents.Invoke(eventName);
    }
}

