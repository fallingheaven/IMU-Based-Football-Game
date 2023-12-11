using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ConnectionSuccess : MonoBehaviour
{
    public VoidEventSO onConnectedEventSO;
    public GameObject successUI;
    public float hideTime;
    public float revealTime;
    
    private void OnEnable()
    {
        onConnectedEventSO.onEventRaised += HideLoad;
        successUI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        onConnectedEventSO.onEventRaised -= HideLoad;
    }

    private void HideLoad()
    {
        StartCoroutine(HideLoadingUI());
    }
    
    private IEnumerator HideLoadingUI()
    {
        // 遍历所有孩子
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            Tween tween = child.transform.DOScale(0f, hideTime).SetEase(Ease.OutExpo).SetUpdate(true);
        }
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
        
        StartCoroutine(RevealSuccessUI());
        yield return null;
    }

    private IEnumerator RevealSuccessUI()
    {
        successUI.gameObject.SetActive(true);
        successUI.transform.localScale = Vector3.zero;
        
        Tween tween = successUI.transform.DOScale(1f, revealTime).SetEase(Ease.OutExpo).SetUpdate(true);
        yield return tween.WaitForCompletion();
        tween = successUI.transform.DOScale(0f, hideTime).SetEase(Ease.OutExpo).SetUpdate(true);
        yield return tween.WaitForCompletion();
        
        gameObject.SetActive(false);
    }
}
