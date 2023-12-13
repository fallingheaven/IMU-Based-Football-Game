using TMPro;
using UnityEngine;

public class UpdateLevelInfo : MonoBehaviour
{
    public GameTarget gameTarget;
    public TextMeshProUGUI text;
    private char[] _scoreString = { '0', '0', '0', '0', '0', '0' };

    [Header("事件监听")] 
    public VoidEventSO startLevelEventSO;

    private void OnEnable()
    {
        startLevelEventSO.onEventRaised += UpdateLevelInfoPanel;
    }

    private void OnDisable()
    {
        startLevelEventSO.onEventRaised -= UpdateLevelInfoPanel;
    }

    private void UpdateLevelInfoPanel()
    {
        var tmp = gameTarget.currentTarget;
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
        
        text.text = $"第{gameTarget.currentLevel}天      目标：{new string(_scoreString)}";
    }
}
