using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public FloatEventSO scoreChangeEventSO;
    public float score;
    private TextMeshProUGUI _scoreInfo;
    [Header("事件监听")]
    public FloatEventSO updateScoreEventSO;
    
    private void OnEnable()
    {
        updateScoreEventSO.OnEventRaised += UpdateScore;
    }

    private void OnDisable()
    {
        updateScoreEventSO.OnEventRaised -= UpdateScore;
    }

    private void Start()
    {
        score = 0;
        _scoreInfo = GetComponent<TextMeshProUGUI>();
    }
    
    private void FixedUpdate()
    {
        _scoreInfo.text = $"分数：{score}";
    }

    private void UpdateScore(float a)
    {
        score += a;
        scoreChangeEventSO.RaiseEvent(score);
    }
}
