using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComboRecord : MonoBehaviour
{
    public float fadeTime;
    public float comboGap;
    public ScoreDataSO scoreData;
    
    private TimeCounter _timeCounter;
    private TextMeshProUGUI _textMeshProUGUI;
    
    [Header("事件监听")]
    public FloatEventSO updateScoreEventSO;
    public VoidEventSO nextLevelEventSO;
    public VoidEventSO startLevelEventSO;

    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _timeCounter = new TimeCounter();
        
        updateScoreEventSO.OnEventRaised += RefreshCombo;
        nextLevelEventSO.onEventRaised += _timeCounter.Pause;
        startLevelEventSO.onEventRaised += _timeCounter.Resume;
    }

    private void OnDisable()
    {
        updateScoreEventSO.OnEventRaised -= RefreshCombo;
        nextLevelEventSO.onEventRaised -=  _timeCounter.Pause;
        startLevelEventSO.onEventRaised -=  _timeCounter.Resume;
    }

    private void FixedUpdate()
    {
        // Debug.Log(scoreData.combo);
        _timeCounter.FixedUpdate();
        
        if (_timeCounter.End())
        {
            scoreData.combo = 0;
            _textMeshProUGUI.text = "";
            return;
        }
        
        if (scoreData.combo >= 1)
        {
            _textMeshProUGUI.text = $"×{scoreData.combo}";
        }
    }

    private void RefreshCombo(float addedScore)
    {
        // Debug.Log(addedScore);
        if (addedScore <= 0)
        {
            scoreData.combo = 0;
            _textMeshProUGUI.text = "";
            return;
        }

        scoreData.combo++;
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
