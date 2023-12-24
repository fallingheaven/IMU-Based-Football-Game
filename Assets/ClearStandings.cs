using UnityEngine;

public class ClearStandings : MonoBehaviour
{
    public ScoreDataSO scoreData;
    public GameObject content;

    public void ClearStanding()
    {
        scoreData.previousScore.Clear();
        for (var i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
}
