using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameObjectFloatFloatEventSO")]
public class GameObjectFloatFloatEventSO : ScriptableObject
{
    public UnityAction<GameObject, float> onEventRaised;

    public void RaiseEvent(GameObject gameObject, float f)
    {
        onEventRaised?.Invoke(gameObject, f);
    }
}
