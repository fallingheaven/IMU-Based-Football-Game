using TMPro;
using UnityEngine;

public class RecordScore : MonoBehaviour
{
    public ScoreDataSO scoreData;
    public SettingDataSO settingData;

    public void AddRecord(string playerName)
    {
        // Debug.Log(GetComponent<TMP_InputField>().text);
        scoreData.previousScore.Add(new ScoreInfo(GetComponent<TMP_InputField>()?.text, settingData.difficulty, scoreData.currentScore));
    }
}
