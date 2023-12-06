using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    public SettingDataSO settingData;
    public Vector2 range;

    private void Start()
    {
        GetComponent<Slider>().value = settingData.imuSensitivity;
    }

    public void Change(float value)
    {
        
        settingData.imuSensitivity = range.x + value * (range.y - range.x);
        // Debug.Log(value);
        // Debug.Log(settingData.imuSensitivity);
    }
}
