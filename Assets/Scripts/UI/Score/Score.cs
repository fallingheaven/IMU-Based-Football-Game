using TMPro;
using UnityEngine;

// 算是单例模式
public class Score : MonoBehaviour
{
    // public float score;
    public ScoreDataSO scoreData;
    public FloatEventSO scoreChangeEventSO;
    
    private char[] _scoreString = { '0', '0', '0', '0', '0', '0' };
    private TextMeshProUGUI _scoreInfo;// 用于更新分数板
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
        scoreData.currentScore = 0;
        _scoreInfo = GetComponent<TextMeshProUGUI>();
    }
    
    private void FixedUpdate()
    {
        var tmp = scoreData.currentScore;
        if (tmp >= 999999) tmp = 999999;
        
        var i = _scoreString.Length - 1;
        while (tmp >= 1)
        {
            _scoreString[i] = (char)('0' + tmp % 10);
            tmp /= 10;
            i--;
        }

        for (; i >= 0; i--)
        {
            _scoreString[i] = '0';
        }
        
        _scoreInfo.text = $"分数：        {new string(_scoreString)}"; // 8个空格
    }

    private void UpdateScore(float a)
    {
        scoreData.currentScore += a * ReturnComboCoefficient(scoreData.combo);
        if (scoreData.currentScore < 0) scoreData.currentScore = 0;
        
        scoreChangeEventSO.RaiseEvent(scoreData.currentScore);
    }

    private float ReturnComboCoefficient(int combo)
    {
        var coefficient = 1f;
        if (scoreData.combo >= 5)
        {
            coefficient = 1.5f;
        }
        else if (scoreData.combo >= 15)
        {
            coefficient = 2f;
        }
        else if (scoreData.combo >= 30)
        {
            coefficient = 3f;
        }
        else if (scoreData.combo >= 50)
        {
            coefficient = 5f;
        }

        return coefficient;
    }
}
