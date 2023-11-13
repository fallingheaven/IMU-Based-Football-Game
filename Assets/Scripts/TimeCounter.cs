using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimeCounter: MonoBehaviour
{
    private TimeCounter _timeCounter;
    private float _currentTime;
    private bool _onCount = false;
    private UnityEvent _eventToDo;
    public TimeCounter Instance
    {
        get
        {
            _timeCounter ??= this;
            return _timeCounter;
        }
    }
    
    private void Update()
    {
        if (!_onCount)
        {
            return;
        }
        
        _currentTime -= Time.deltaTime;

        if (_currentTime > 0)
        {
            return;
        }
        
        _onCount = false;
        _eventToDo?.Invoke();
    }
    
    public void StartTimeCount(float duration, UnityEvent eventToDo = null)
    {
        _onCount = true;
        _currentTime = duration;
        _eventToDo = eventToDo;
    }

    public void EndTimeCount()
    {
        _onCount = false;
    }
}
