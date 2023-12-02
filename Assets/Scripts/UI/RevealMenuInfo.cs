using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RevealMenuInfo : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    
    public void Reveal()
    {
        gameObject.SetActive(true);
        StartCoroutine(RevealPanel());
    }

    public void Hide()
    {
        StartCoroutine(HidePanel());
    }
    
    private IEnumerator RevealPanel()
    {
        transform.DOLocalMoveX(1400, fadeTime).SetEase(Ease.OutExpo);
        yield return null;
    }

    private IEnumerator HidePanel()
    {
        Tweener tweener = transform.DOLocalMoveX(1900, fadeTime).SetEase(Ease.Linear);
        yield return tweener.WaitForCompletion();
        
        gameObject.SetActive(false);
    }
}
