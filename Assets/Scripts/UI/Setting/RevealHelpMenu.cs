using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealHelpMenu : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    private CanvasGroup _canvasGroup;
    private bool _fading = false;
    private bool _finished = false;
    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    public void Reveal()
    {
        if (_fading || _finished) return;
        _fading = true;
        
        gameObject.SetActive(true);
        transform.localScale = new Vector3(0.98f, 0.98f, 0.98f);
        
        StartCoroutine(RevealMenu());
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
    public void SetActive()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        StartCoroutine(HideMenu());
    }

    private IEnumerator RevealMenu()
    {
        // Tweener tweener = transform.DOLocalMoveY(0, fadeTime).SetEase(Ease.OutExpo);
        Tweener tweener = _canvasGroup.DOFade(1, fadeTime).SetEase(Ease.OutExpo).SetUpdate(true);
        yield return new WaitForSecondsRealtime(fadeTime / 4);
        transform.DOScale(1f, fadeTime / 2).SetEase(Ease.Linear).SetUpdate(true);
        yield return tweener.WaitForCompletion();
        
        _fading = false;
        _finished = true;
    }

    private IEnumerator HideMenu()
    {
        // Tweener tweener = transform.DOScale(0, fadeTime).SetEase(Ease.OutExpo);
        Tweener tweener = _canvasGroup.DOFade(0, fadeTime).SetEase(Ease.OutExpo).SetUpdate(true);
        transform.DOScale(0.95f, fadeTime / 2).SetEase(Ease.Linear).SetUpdate(true);
        yield return tweener.WaitForCompletion();
        

        _finished = false;
        if (_fading) yield break;
        gameObject.SetActive(false);
    }
}