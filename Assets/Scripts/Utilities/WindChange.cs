using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class WindChange : MonoBehaviour
{
    public Vector2 windRange;
    public float changeGap;
    public WindSO windSO;
    private UnityEvent myEvent = new UnityEvent();

    private readonly TimeCounter _timeCounter = new TimeCounter();

    private void OnEnable()
    {
        myEvent.AddListener(ChangeWind);
        _timeCounter.StartTimeCount(changeGap, myEvent);
    }

    private void OnDisable()
    {
        myEvent.RemoveListener(ChangeWind);
    }

    private void FixedUpdate()
    {
        _timeCounter.FixedUpdate();
    }

    private void ChangeWind()
    {
        windSO.windForce.x = Random.Range(windRange.x, windRange.y);
        _timeCounter.StartTimeCount(changeGap, myEvent);
        Debug.Log(windSO.windForce.x);
    }
}