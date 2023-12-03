using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealPauseMenu : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private bool _fading = false;
    private bool _finished = false;

    public void Reveal()
    {
        if (_fading || _finished) return;
        _fading = true;
        
        var _localPosition = transform.localPosition;
        transform.localPosition = new Vector3(_localPosition.x, -1080, _localPosition.z);
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        
        StartCoroutine(RevealMenu());
    }

    public void Hide()
    {
        StartCoroutine(HideMenu());
    }

    private IEnumerator RevealMenu()
    {
        Tweener tweener = transform.DOLocalMoveY(0, fadeTime).SetEase(Ease.OutExpo);
        yield return tweener.WaitForCompletion();
        _fading = false;
        _finished = true;
    }

    private IEnumerator HideMenu()
    {
        Tweener tweener = transform.DOScale(0, fadeTime).SetEase(Ease.OutExpo);
        yield return tweener.WaitForCompletion();

        _finished = false;
        if (_fading) yield break;
        gameObject.SetActive(false);
    }
}
