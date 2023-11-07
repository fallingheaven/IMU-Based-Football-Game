using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsCheck : MonoBehaviour
{
    // public UnityEvent<int> addScore;
    public UnityEvent<GameObject> onMiss, onGoal;
    public LayerMask goalLayer, missLayer, kickLayer;
    private ShootFootball _shoot;
    public int addedScore = 1;

    private void Start()
    {
        _shoot = GetComponent<ShootFootball>();
        // addScore.AddListener(GameObject.Find("Score").GetComponent<Score>().UpdateScore);
        onGoal.AddListener(GameObject.Find("Football Server").GetComponent<CharacterPool>().ReturnCharacterToPool);
        onMiss.AddListener(GameObject.Find("Football Server").GetComponent<CharacterPool>().ReturnCharacterToPool);
        goalLayer = LayerMask.NameToLayer("Goal");
        missLayer = LayerMask.NameToLayer("Miss");
        // kickLayer = LayerMask.NameToLayer("Kick");
    }

    private void OnTriggerEnter(Collider col)
    {
        // Debug.Log("Trigger");
        // 进到球门里就得分
        if (col.gameObject.layer == goalLayer)
        {
            Debug.Log("Goal!");
            Score.UpdateScore(addedScore);
            onGoal.Invoke(gameObject);
        }
        else if (col.gameObject.layer == missLayer)
        {
            // Debug.Log("Miss~");
            onMiss.Invoke(gameObject);
        }
    }
}
