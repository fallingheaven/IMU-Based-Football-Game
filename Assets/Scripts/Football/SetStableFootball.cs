using System;
using UnityEngine;

// 用于把球静止地放在某个位置
public class SetStableFootball : MonoBehaviour
{
    private bool _pause = false;
    private CharacterPool _characterPool;
    public Vector3 initPosition;
    
    [Header("事件监听")]
    public VoidEventSO pauseGameEventSO;
    public VoidEventSO resumeEventSO;
    private void OnEnable()
    {
        _characterPool = GetComponent<CharacterPool>();
        pauseGameEventSO.onEventRaised += Pause;
        resumeEventSO.onEventRaised += Resume;
    }

    private void OnDisable()
    {
        pauseGameEventSO.onEventRaised -= Pause;
        resumeEventSO.onEventRaised -= Resume;
    }

    private void FixedUpdate()
    {
        if (_characterPool.availableNum <= 0 || _pause) return;
        
        var football = _characterPool.GetCharacterFromPool();
        
        var _rigidbody = football.GetComponent<Rigidbody>();
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        football.transform.position = initPosition;
    }
    
    private void Pause()
    {
        _pause = true;
    }

    private void Resume()
    {
        _pause = false;
    }
}
