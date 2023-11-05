using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    private static Score _instance;
    private static long _score;
    private VisualElement _scoreBoard;
    private Label _scoreInfo;
        
    public static Score Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Score();
            }

            return _instance;
        }
    }
    
    private void Start()
    {
        _score = 0;
        _scoreBoard = GetComponent<UIDocument>().rootVisualElement;
        _scoreInfo = _scoreBoard.Q<Label>("ScoreLabel");
    }
    
    private void FixedUpdate()
    {
        Debug.Log(_score);
        _scoreInfo.text = $"分数：{_score}";
    }
    
    public static void UpdateScore(int a)
    {
        _score += a;
    }
}
