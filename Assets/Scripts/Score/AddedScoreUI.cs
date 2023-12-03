using System;
using TMPro;
using UnityEngine;

public class AddedScoreUI : MonoBehaviour
{
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
        var score = Instantiate(addedScoreGameObject, transform, true);
        score.transform.localPosition = new Vector3(20 - GetLength(Math.Abs(addedScore)) * 40, 25, 0);
        
        score.GetComponent<TextMeshProUGUI>().text = (addedScore >= 0) switch
        {
            true  => $"+{addedScore}",
            false => $"-{-addedScore}"
        };
        var scoreAnimation = score.GetComponent<RevealAddedScore>();
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
