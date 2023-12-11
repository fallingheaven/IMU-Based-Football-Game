using TMPro;
using UnityEngine;

public class InitialDirectionText : MonoBehaviour
{
    public SettingDataSO settingData;
    
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = $"{SharedDictionary.DirectionDictionary[settingData.direction]}";
    }
}
