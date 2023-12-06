using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealPauseMenu : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private CanvasGroup _canvasGroup;
    private bool _fading = false;
    private bool _finished = false;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Reveal()
    {
        if (_fading || _finished) return;
        _fading = true;
        
        // var _localPosition = transform.localPosition;
        // transform.localPosition = new Vector3(_localPosition.x, -1080, _localPosition.z);
        // transform.localScale = Vector3.one;
        transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        gameObject.SetActive(true);
        
        StartCoroutine(RevealMenu());
    }

    public void Hide()
    {
        StartCoroutine(HideMenu());
    }

    private IEnumerator RevealMenu()
    {
        // Tweener tweener = transform.DOLocalMoveY(0, fadeTime).SetEase(Ease.OutExpo);
        Tweener tweener = _canvasGroup.DOFade(1, fadeTime).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(fadeTime / 4);
        transform.DOScale(1f, fadeTime / 2).SetEase(Ease.Linear);
        yield return tweener.WaitForCompletion();
        
        _fading = false;
        _finished = true;
    }

    private IEnumerator HideMenu()
    {
        // Tweener tweener = transform.DOScale(0, fadeTime).SetEase(Ease.OutExpo);
        Tweener tweener = _canvasGroup.DOFade(0, fadeTime).SetEase(Ease.OutExpo);
        transform.DOScale(0.95f, fadeTime / 2).SetEase(Ease.Linear);
        yield return tweener.WaitForCompletion();
        

        _finished = false;
        if (_fading) yield break;
        gameObject.SetActive(false);
    }
}
