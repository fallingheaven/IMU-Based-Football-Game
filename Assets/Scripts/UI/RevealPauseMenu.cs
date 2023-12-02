using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealPauseMenu : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private Vector3 _transformLocalPosition;

    private void OnEnable()
    {
        _transformLocalPosition = transform.localPosition;
    }

    public void Reveal()
    {
        if (gameObject.activeSelf) return;
        
        transform.localPosition = new Vector3(_transformLocalPosition.x, -1080, _transformLocalPosition.z);
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
        transform.DOLocalMoveY(0, fadeTime).SetEase(Ease.OutExpo);
        yield return null;
    }

    private IEnumerator HideMenu()
    {
        Tweener tweener = transform.DOScale(0, fadeTime).SetEase(Ease.OutExpo);
        yield return tweener.WaitForCompletion();
        
        gameObject.SetActive(false);
    }
}
