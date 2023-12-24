using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealGameSuccessPanel : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private CanvasGroup _canvasGroup;
    private bool _fading = false;

    [Header("事件监听")] 
    public VoidEventSO gameSuccessEventSO;

    private void OnEnable()
    {
        gameSuccessEventSO.onEventRaised += Reveal;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        gameSuccessEventSO.onEventRaised -= Reveal;
    }

    public void Reveal()
    {
        // Debug.Log(1);
        if (_fading) return;
        _fading = true;
        
        // Debug.Log(2);
        transform.localScale = new Vector3(0.58f, 0.58f, 0.58f);
        
        StartCoroutine(RevealPanel());
    }

    public void Hide()
    {
        StartCoroutine(HidePanel());
    }

    private IEnumerator RevealPanel()
    {
        Tweener tweener = _canvasGroup.DOFade(1, fadeTime).SetEase(Ease.OutExpo).SetUpdate(true);
        yield return new WaitForSecondsRealtime(fadeTime / 4);
        transform.DOScale(0.6f, fadeTime / 2).SetEase(Ease.Linear).SetUpdate(true);
        
        _fading = false;
    }

    private IEnumerator HidePanel()
    {
        Tweener tweener = _canvasGroup.DOFade(0, fadeTime).SetEase(Ease.OutExpo).SetUpdate(true);
        transform.DOScale(0.5f, fadeTime / 2).SetEase(Ease.Linear).SetUpdate(true);
        yield return tweener.WaitForCompletion();

        if (_fading) yield break;
        transform.localScale = Vector3.zero;
    }
}
