using System;
using UnityEngine;

public class AudioDefinition : MonoBehaviour
{
    public PlayAudioEventSO playAudioEventSO;
    public AudioClip[] audioClip;
    public bool playOnEnable;
    public bool playOnStart;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudio();
        }
    }

    private void Start()
    {
        if (playOnStart)
        {
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        if (audioClip.Length > 1)
        {
            var kind = new System.Random().Next(0, audioClip.Length);
            playAudioEventSO.RaiseEvent(audioClip[kind]);
        }
        else
        {
            playAudioEventSO.RaiseEvent(audioClip[0]);
        }
    }
}
