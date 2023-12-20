using UnityEngine;

public class KickAnimation : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO kickEventSO;
    public VoidEventSO pauseGameEventSO;
    public VoidEventSO resumeEventSO;
    
    private Animator _animator;
    private bool _pause = false;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        kickEventSO.onEventRaised += PlayKickAnimation;
        pauseGameEventSO.onEventRaised += Pause;
        resumeEventSO.onEventRaised += Resume;
    }

    private void OnDisable()
    {
        kickEventSO.onEventRaised -= PlayKickAnimation;
        pauseGameEventSO.onEventRaised -= Pause;
        resumeEventSO.onEventRaised -= Resume;
    }

    private void PlayKickAnimation()
    {
        if (_pause) return;
        
        _animator.SetTrigger("Kick");
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
