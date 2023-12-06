using TMPro;
using UnityEngine;

// 算是单例模式
public class Score : MonoBehaviour
{
    public float score;
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
        score = 0;
        _scoreInfo = GetComponent<TextMeshProUGUI>();
    }
    
    private void FixedUpdate()
    {
        var tmp = score;
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
        
        // Debug.Log(score);
        // Debug.Log(new string(_scoreString));
        _scoreInfo.text = $"分数：        {new string(_scoreString)}"; // 8个空格
    }

    private void UpdateScore(float a)
    {
        score += a;
        if (score < 0) score = 0;
        
        scoreChangeEventSO.RaiseEvent(score);
    }
}
