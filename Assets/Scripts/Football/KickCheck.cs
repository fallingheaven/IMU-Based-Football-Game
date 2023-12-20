using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    // public LayerMask BallLayer; // 检测的图层，这里是足球图层
    private List<GameObject> _colliders = new List<GameObject>();
    public ParticleSystem kickParticle; // 特效粒子
    public FloatEventSO updateScoreEventSO;
    private bool _pause = false;
        
    [Header("事件监听")] 
    public VoidEventSO imuKickEventSO;
    public VoidEventSO clearColliderEventSO;
    public VoidEventSO pauseGameEventSO;
    public VoidEventSO resumeGameEventSO;

    private void OnEnable()
    {
        imuKickEventSO.onEventRaised += Kick;
        clearColliderEventSO.onEventRaised += ClearCollider;
        pauseGameEventSO.onEventRaised += PauseReceive;
        resumeGameEventSO.onEventRaised += ResumeReceive;
    }

    private void OnDisable()
    {
        imuKickEventSO.onEventRaised -= Kick;
        clearColliderEventSO.onEventRaised -= ClearCollider;
        pauseGameEventSO.onEventRaised -= PauseReceive;
        resumeGameEventSO.onEventRaised -= ResumeReceive;
    }

    private void OnTriggerEnter(Collider col)
    {
        var ball = col.gameObject.GetComponent<Ball>();
        if (ball == null) return;
        
        // 每进入到判定区域就尝试加入到数组
        _colliders.Add(col.gameObject);
    }

    private void OnTriggerExit(Collider col)
    {
        // 出范围就删除
        _colliders.Remove(col.gameObject);
    }

    // 把数组中所有的球都踢出去
    private void Kick()
    {
        if (_pause) return;
        
        // Debug.Log(_colliders.Count);
        // Debug.Log("Shoot!");
        if (_colliders is not { Count: > 0 })
        {
            return;
        }
        
        kickParticle.Play();
        
        foreach (var football in _colliders)
        {
            if (football == null) break;
            
            var ball = football.GetComponent<Ball>();
            
            if (ball.type != BallType.Bomb)
            {
                football.gameObject.GetComponent<ShootFootball>().Shoot();
            }
            else
            {
                updateScoreEventSO.RaiseEvent(ball.score);
                football.gameObject.GetComponent<Explode>().PlayExplodeAnimation();
            }
        }
        
        ClearCollider();
    }

    private void ClearCollider()
    {
        _colliders.Clear();
    }
    
    private void PauseReceive()
    {
        _pause = true;
    }
    
    private void ResumeReceive()
    {
        _pause = false;
    }
}
