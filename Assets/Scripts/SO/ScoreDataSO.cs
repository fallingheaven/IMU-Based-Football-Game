using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataSO/ScoreDataSO")]
public class ScoreDataSO : ScriptableObject
{
    public float currentScore;
    public List<float> previousScore;
}
