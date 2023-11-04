using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score _instance;
    private static long _score;

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

    private void FixedUpdate()
    {
        Debug.Log(_score);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _score = 0;
    }

    public static void UpdateScore(int a)
    {
        _score += a;
    }
}
