using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")] 
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    
    public AudioSource FXSource;
    public AudioSource BGMSource;

    public void OnEnable()
    {
        FXEvent.onEventRaised += OnFXEvent;
        BGMEvent.onEventRaised += OnBGMEvent;
    }
    
    public void OnDisable()
    {
        FXEvent.onEventRaised -= OnFXEvent;
        BGMEvent.onEventRaised -= OnBGMEvent;
    }

    private void OnBGMEvent(AudioClip audioClip)
    {
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip audioClip)
    {
        FXSource.clip = audioClip;
        FXSource.Play();
    }
}
