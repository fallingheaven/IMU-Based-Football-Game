using TMPro;
using UnityEngine;

public class InitialDifficultyDegree : MonoBehaviour
{
    public GameTarget gameTarget;

    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = $"{SharedDictionary.DifficultyDictionary[gameTarget.difficultyDegree]}";
    }
}
