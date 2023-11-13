using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/QuatEventSO")]
public class QuatEventSO : ScriptableObject
{
    public UnityEvent<float[]> onEventRaised;

    public void RaiseEvent(float[] quat)
    {
        onEventRaised?.Invoke(quat);
    }
}
