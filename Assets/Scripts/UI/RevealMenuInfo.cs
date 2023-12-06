using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealMenuInfo : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private bool _fading = false;

    public void Reveal()
    {
        if (_fading) return;
        _fading = true;
        
        var _localPosition = transform.localPosition;
        transform.localPosition = new Vector3(-1900, _localPosition.y, _localPosition.z);
        
        gameObject.SetActive(true);
        StartCoroutine(RevealPanel());
    }

    public void Hide()
    {
        StartCoroutine(HidePanel());
    }
    
    private IEnumerator RevealPanel()
    {
        Tweener tweener = transform.DOLocalMoveX(-1400, fadeTime).SetEase(Ease.OutExpo);
        yield return tweener.WaitForCompletion();
        _fading = false;
    }

    private IEnumerator HidePanel()
    {
        Tweener tweener = transform.DOLocalMoveX(-1900, fadeTime).SetEase(Ease.OutExpo);
        yield return tweener.WaitForCompletion();

        if (_fading) yield break;
        gameObject.SetActive(false);
    }
}
