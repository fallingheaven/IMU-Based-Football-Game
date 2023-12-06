using System;
using TMPro;
using UnityEngine;

public class AddedScoreUI : MonoBehaviour
{
    private GameObject _currentScoreUI;
    [Header("事件监听")]
    public FloatEventSO scoreChangeEventSO;
    public GameObject addedScoreGameObject;

    private void OnEnable()
    {
        scoreChangeEventSO.OnEventRaised += Generate;
    }

    private void OnDisable()
    {
        scoreChangeEventSO.OnEventRaised -= Generate;
    }

    private void Generate(float addedScore)
    {
        if (_currentScoreUI != null)
        {
            Destroy(_currentScoreUI.gameObject);
        }
        
        _currentScoreUI = Instantiate(addedScoreGameObject, transform, true);
        _currentScoreUI.transform.localPosition = new Vector3(20 - GetLength(Math.Abs(addedScore)) * 40, 50, 0);
        
        _currentScoreUI.GetComponent<TextMeshProUGUI>().text = (addedScore >= 0) switch
        {
            true  => $"+{addedScore}",
            false => $"-{-addedScore}"
        };
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
}
