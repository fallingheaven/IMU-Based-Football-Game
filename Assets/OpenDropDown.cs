using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OpenDropDown : MonoBehaviour
{
    // public Button[] direction;
    public List<GameObject> buttons;
    public float fadeTime;
    private bool _reveal;
    
    private void Start()
    {
        DOTween.Init();
    }

    private void OnEnable()
    {
        _reveal = false;
    }

    private void OnDisable()
    {
        _reveal = true;
    }

    public void OnSelected()
    {
        if (!_reveal)
        {
            StartCoroutine(RevealButtons());
        }

        if (_reveal)
        {
            StartCoroutine(HideButtons());
        }
    }

    private IEnumerator RevealButtons()
    {
        foreach (var button in buttons)
        {
            button.SetActive(true);
            button.transform.localScale = Vector3.zero;
        }

        foreach (var button in buttons)
        {
            button.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            
            yield return new WaitForSeconds(0.25f);
        }

        _reveal = true;
    }
    
    private IEnumerator HideButtons()
    {
        for (var i = buttons.Count - 1; i >= 0; i--)
        {
            var button = buttons[i];
            button.transform.DOScale(0f, fadeTime).SetEase(Ease.OutExpo);
            yield return new WaitForSeconds(0.25f);
            button.SetActive(false);
        }

        _reveal = false;
    }
}
