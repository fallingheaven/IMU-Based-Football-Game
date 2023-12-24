using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevealAllStandings : MonoBehaviour
{
    public ScoreDataSO scoreData;
    public GameObject record;
    
    private void OnEnable()
    {
        var comparer = new ScoreInfo("", Difficulty.Easy, 0f);
        scoreData.previousScore.Sort(comparer.Compare);
        
        foreach (var scoreInfo in scoreData.previousScore)
        {
            var newRecord = GameObject.Instantiate(record, transform);
            newRecord.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = $"{scoreInfo.playerName}";
            newRecord.transform.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = 
                                                        $"{SharedDictionary.DifficultyDictionary[scoreInfo.difficulty]}";
            newRecord.transform.Find("Score").GetComponent<TextMeshProUGUI>().text =
                                                        $"{GetScoreString(scoreInfo.score)}";
        }
    }

    private void OnDisable()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private string GetScoreString(float score)
    {
        char[] scoreString = { '0', '0', '0', '0', '0', '0' };

        var tmp = score;
        if (tmp >= 999999) tmp = 999999;
        
        var i = scoreString.Length - 1;
        while (tmp >= 1)
        {
            scoreString[i] = (char)('0' + tmp % 10);
            tmp /= 10;
            i--;
        }

        for (; i >= 0; i--)
        {
            scoreString[i] = '0';
        }

        return new string(scoreString);
    }
}
