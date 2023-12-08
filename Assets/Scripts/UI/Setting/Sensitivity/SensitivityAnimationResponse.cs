using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityAnimationResponse : MonoBehaviour
{
    public VoidEventSO imuKickEventSO;
    public float fadeTime;
    public Image image;

    private void OnEnable()
    {
        image.transform.localScale = Vector3.zero;
        imuKickEventSO.onEventRaised += Play;
    }

    private void OnDisable()
    {
        imuKickEventSO.onEventRaised -= Play;
    }

    private void Play()
    {
        // Debug.Log("播放动画");
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        image.DOKill();
        
        Tween tween = image.transform.DOScale(4f, fadeTime).SetEase(Ease.OutExpo);
        yield return tween.WaitForCompletion();
        image.transform.DOScale(0f, fadeTime).SetEase(Ease.OutExpo);
    }
}
