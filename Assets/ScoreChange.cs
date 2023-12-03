using UnityEngine;

// 用于测试
public class ScoreChange : MonoBehaviour
{
    public float addedScore;
    public FloatEventSO updateScoreEventSO;

    public void Add()
    {
        updateScoreEventSO.RaiseEvent(addedScore);
    }
}
