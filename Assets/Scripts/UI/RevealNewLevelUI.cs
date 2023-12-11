using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RevealNewLevelUI : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    public UnityEvent startServeFootball;
    public VoidEventSO startLevelEventSO;

    private void OnEnable()
    {
        startLevelEventSO.onEventRaised += Reveal;
    }

    private void OnDisable()
    {
        startLevelEventSO.onEventRaised -= Reveal;
    }

    public void Reveal()
    {
        // Debug.Log("新一关");
        // gameObject.SetActive(true);
        
        transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z);
        StartCoroutine(RevealPanel());
    }

    public void Hide()
    {
        StartCoroutine(HidePanel());
    }

    private IEnumerator RevealPanel()
    {
        Tween tween = transform.DOScaleY(0.25f, fadeTime).SetEase(Ease.OutExpo);
        yield return tween.WaitForCompletion();
        Hide();
    }

    private IEnumerator HidePanel()
    {
        Tween tween = transform.DOScaleY(0f, fadeTime / 2).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        startServeFootball?.Invoke();
        // gameObject.SetActive(false);
    }
}
