using UnityEngine;
using UnityEngine.Events;

// 计时器，暂时用不上
public class TimeCounter
{
    private TimeCounter _timeCounter;
    private float _currentTime;
    private float _totalTime;
    private bool _onCount = false;
    private UnityEvent _eventToDo;
    private bool _pause;
    
    public void FixedUpdate()
    {
        if (!_onCount || _pause)
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
        _pause = false;
        _onCount = true;
        _currentTime = duration;
        _totalTime = duration;
        _eventToDo = eventToDo;
    }

    public void EndTimeCount()
    {
        _onCount = false;
    }

    public bool End()
    {
        return !_onCount;
    }

    public void Pause()
    {
        _pause = true;
    }

    public void Resume()
    {
        _pause = false;
        StartTimeCount(_totalTime);
    }
}
