using System;
using TMPro;
using UnityEngine;

public class AddedScoreUI : MonoBehaviour
{
    public float offset;
    public ScoreDataSO scoreData;
    private GameObject _currentScoreUI;
    
    [Header("事件监听")]
    public FloatEventSO updateScoreEventSO;
    public GameObject addedScoreGameObject;

    private void OnEnable()
    {
        updateScoreEventSO.OnEventRaised += Generate;
    }

    private void OnDisable()
    {
        updateScoreEventSO.OnEventRaised -= Generate;
    }

    private void Generate(float addedScore)
    {
        if (_currentScoreUI != null)
        {
            Destroy(_currentScoreUI.gameObject);
        }
        
        _currentScoreUI = Instantiate(addedScoreGameObject, transform, true);
        _currentScoreUI.transform.localPosition = new Vector3(offset - GetLength(Math.Abs(addedScore)) * 40, 50, 0);

        _currentScoreUI.GetComponent<TextMeshProUGUI>().text =
            $"+{addedScore}×{ReturnComboCoefficient(scoreData.combo)}";
        
        var scoreAnimation = _currentScoreUI.GetComponent<RevealAddedScore>();
        scoreAnimation.Reveal();
    }

    private int GetLength(float a)
    {
        var tmp = a;
        var length = 0;
        while (tmp >= 1)
        {
            tmp /= 10;
            length++;
        }

        return length - 1;
    }
    
    private float ReturnComboCoefficient(int combo)
    {
        var coefficient = 1f;
        if (scoreData.combo >= 5)
        {
            coefficient = 1.5f;
        }
        else if (scoreData.combo >= 15)
        {
            coefficient = 2f;
        }
        else if (scoreData.combo >= 30)
        {
            coefficient = 3f;
        }
        else if (scoreData.combo >= 50)
        {
            coefficient = 5f;
        }

        return coefficient;
    }
}
