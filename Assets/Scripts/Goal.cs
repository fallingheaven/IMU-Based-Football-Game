using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent<int> addScore;
    public LayerMask goalLayer;
    public int score = 1;

    private void Start()
    {
        goalLayer = LayerMask.NameToLayer("Goal");
    }

    private void OnTriggerEnter(Collider col)
    {
        // 进到球门里就得分
        if (col.gameObject.layer == goalLayer)
        {
            Debug.Log("Goal!");
            addScore?.Invoke(score);
        }
    }
}
