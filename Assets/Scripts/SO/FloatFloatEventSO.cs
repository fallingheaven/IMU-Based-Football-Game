using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FloatFloatEventSO")]
public class FloatFloatEventSO : ScriptableObject
{
    public UnityAction<float, float> onEventRaised;

    public void RaiseEvent(float a, float b)
    {
        onEventRaised?.Invoke(a, b);
    }
}
