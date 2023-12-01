using UnityEngine;
using UnityEngine.Events;

// 计时器，暂时用不上
public class TimeCounter
{
    private TimeCounter _timeCounter;
    private float _currentTime;
    private bool _onCount = false;
    private UnityEvent _eventToDo;
    
    public void FixedUpdate()
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
