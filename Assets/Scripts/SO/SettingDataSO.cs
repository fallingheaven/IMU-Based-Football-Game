using UnityEngine;

[CreateAssetMenu(menuName = "DataSO/SettingData")]
public class SettingDataSO : ScriptableObject
{
    public float imuSensitivity = 1000f;
    public Direction direction = Direction.Null;
    public Difficulty difficulty = Difficulty.Easy;
}
