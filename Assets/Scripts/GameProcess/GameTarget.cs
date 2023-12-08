using UnityEngine;

[CreateAssetMenu(menuName = "DataSO/GameTarget")]
public class GameTarget : ScriptableObject
{
    public float initialTarget;
    public float currentTarget;
    public float difficultyDegree;
}
