using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameObjectEventSO")]
public class GameObjectEventSO : ScriptableObject
{
    public UnityAction<GameObject> OnEventRaised;

    public void RaiseEvent(GameObject _gameObject)
    {
        OnEventRaised?.Invoke(_gameObject);
    }
}
