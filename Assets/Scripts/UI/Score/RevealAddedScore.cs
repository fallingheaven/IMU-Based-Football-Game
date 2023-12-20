using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class RevealAddedScore : MonoBehaviour, IRevealUI
{
    public float fadeTime;
    public float fadeDistance;
    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Reveal()
    {
        gameObject.SetActive(true);
        StartCoroutine(RevealScore());
    }

    public void Hide()
    {
        
    }

    private IEnumerator RevealScore()
    {
        transform.DOLocalMoveY(transform.localPosition.y + fadeDistance, fadeTime).SetEase(Ease.Linear);
        yield return new WaitForSeconds(fadeTime / 2);

        Tweener tweener = _text.DOColor(Color.clear, fadeTime / 2).SetEase(Ease.Linear);
        yield return tweener.WaitForCompletion();

        // gameObject.SetActive(false);
        Destroy(gameObject);
    }
    
}
