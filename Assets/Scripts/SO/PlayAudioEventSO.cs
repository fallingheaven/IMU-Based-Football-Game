using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    public UnityAction<AudioClip> onEventRaised;

    public void RaiseEvent(AudioClip audioClip)
    {
        onEventRaised?.Invoke(audioClip);
    }
}
