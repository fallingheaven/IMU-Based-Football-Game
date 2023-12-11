using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    public SettingDataSO settingData;
    public Vector2 range;

    private void OnEnable()
    {
        GetComponent<Slider>().value = settingData.imuSensitivity / (range.y - range.x);
    }

    public void Change(float value)
    {
        settingData.imuSensitivity = range.x + value * (range.y - range.x);
        // Debug.Log(value);
        // Debug.Log(settingData.imuSensitivity);
    }
}
