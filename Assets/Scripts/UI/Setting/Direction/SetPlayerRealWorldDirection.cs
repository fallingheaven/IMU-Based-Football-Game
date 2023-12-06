using TMPro;
using UnityEngine;

public class SetPlayerRealWorldDirection : MonoBehaviour
{
    public OpenDropDown openDropDown;
    public SettingDataSO settingData;
    public Direction direction;
    public TextMeshProUGUI text;
    
    public void ChangeDirection()
    {
        settingData.direction = direction;
        text.text = $"{SharedDictionary.directionDictionary[settingData.direction]}";
        // Debug.Log(text.text);
        openDropDown.OnSelected();
        // Debug.Log(direction);
    }
    
}
