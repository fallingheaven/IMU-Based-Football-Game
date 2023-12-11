using TMPro;
using UnityEngine;

public class SetDifficultyDegree : MonoBehaviour
{
    public OpenDropDown openDropDown;
    public GameTarget gameTarget;
    public Difficulty difficulty;
    public TextMeshProUGUI text;
    public GameManager gameManager;
    
    public void ChangeDifficulty()
    {
        gameManager.ChangeLevelDifficulty(difficulty);
        // gameTarget.difficultyDegree = difficulty;
        text.text = $"{SharedDictionary.DifficultyDictionary[gameTarget.difficultyDegree]}";
        openDropDown.OnSelected();
    }
    
}