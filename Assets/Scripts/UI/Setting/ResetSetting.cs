using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetSetting : MonoBehaviour
{
    public SettingDataSO settingData;
    public SettingDataSO resetData;
    public Slider sensitivitySlider;
    public TextMeshProUGUI text;
    
    public void ResetSettings()
    {
        settingData.imuSensitivity = resetData.imuSensitivity;
        settingData.direction = resetData.direction;
        sensitivitySlider.value = 0f;
        text.text = SharedDictionary.DirectionDictionary[resetData.direction];
    }
}
