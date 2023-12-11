using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RevealSensitivityPanel : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private CanvasGroup _canvasGroup;
    private bool _fading = false;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Reveal()
    {
        if (_fading) return;
        _fading = true;

        transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        
        gameObject.SetActive(true);
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
        gameObject.SetActive(false);
    }
}
