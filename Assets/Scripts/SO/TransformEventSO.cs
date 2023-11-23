using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/TransformEventSO")]
public class TransformEventSO : ScriptableObject
{
    public UnityAction<Transform> onEventRaised;

    public void RaiseEvent(Transform trans)
    {
        onEventRaised?.Invoke(trans);
    }
}
