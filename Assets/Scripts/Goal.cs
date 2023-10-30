using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent<int> addScore;
    

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Goal")
        {
            Debug.Log("Goal!");
            addScore?.Invoke(1);
        }
    }
}
