using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetSetting : MonoBehaviour
{
    public SettingDataSO settingData;
    public SettingDataSO resetData;
    public Slider sensitivitySlider;
    public TextMeshProUGUI directionText;
    public TextMeshProUGUI difficultyText;
    
    public void ResetSettings()
    {
        settingData.imuSensitivity = resetData.imuSensitivity;
        sensitivitySlider.value = 0f;
        
        settingData.direction = resetData.direction;
        directionText.text = SharedDictionary.DirectionDictionary[resetData.direction];

        settingData.difficulty = resetData.difficulty;
        difficultyText.text = SharedDictionary.DifficultyDictionary[resetData.difficulty];
    }
}
