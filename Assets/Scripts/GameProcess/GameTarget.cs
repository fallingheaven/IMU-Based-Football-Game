using UnityEngine;

[CreateAssetMenu(menuName = "DataSO/GameTarget")]
public class GameTarget : ScriptableObject
{
    public float currentLevel; // 从0开始
    public float initialTarget;
    public float currentTarget;
    public Difficulty difficultyDegree;
}
