using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")] 
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;
    
    public AudioSource FXSource;
    public AudioSource BGMSource;

    // 固定写法
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

    // 替换BGM并播放
    private void OnBGMEvent(AudioClip audioClip)
    {
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }

    // 替换音效并播放
    private void OnFXEvent(AudioClip audioClip)
    {
        FXSource.clip = audioClip;
        FXSource.Play();
    }
}
