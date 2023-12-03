using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComboRecord : MonoBehaviour
{
    public float fadeTime;
    public float comboGap;
    
    private TimeCounter _timeCounter;
    private int _combo = 0;
    private TextMeshProUGUI _textMeshProUGUI;
    [Header("事件监听")]
    public FloatEventSO scoreChangeEventSO;

    private void Start()
    {
        _timeCounter = new TimeCounter();
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        scoreChangeEventSO.OnEventRaised += RefreshCombo;
    }

    private void OnDisable()
    {
        scoreChangeEventSO.OnEventRaised -= RefreshCombo;
    }

    private void FixedUpdate()
    {
        _timeCounter.FixedUpdate();
        
        if (_timeCounter.End())
        {
            _combo = 0;
            _textMeshProUGUI.text = "";
            return;
        }
        
        if (_combo >= 1)
        {
            _textMeshProUGUI.text = $"×{_combo}";
        }
    }

    private void RefreshCombo(float addedScore)
    {
        _combo++;
        _timeCounter.StartTimeCount(comboGap);
        StartCoroutine(RefreshAnimation());
    }

    private IEnumerator RefreshAnimation()
    {
        transform.DOKill();
        
        Tweener tweener = transform.DOScale(1.15f, fadeTime).SetEase(Ease.OutExpo);
        yield return tweener.WaitForCompletion();
        transform.DOScale(1f, fadeTime / 2).SetEase(Ease.Linear);
    }
}
