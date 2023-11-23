using UnityEngine;

// 相当于是音频的属性
public class AudioDefinition : MonoBehaviour
{
    // 播放声音的事件
    public PlayAudioEventSO playAudioEventSO;
    // 内含的声音集合
    public AudioClip[] audioClip;
    // 用于音效
    public bool playOnEnable;
    // 用于BGM
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
        if (audioClip.Length > 1)// 不只一个音效选择
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
