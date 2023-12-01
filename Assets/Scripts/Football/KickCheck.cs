using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    public LayerMask checkedLayer; // 检测的图层，这里是足球图层
    private List<GameObject> _colliders = new List<GameObject>();
    public ParticleSystem kickParticle; // 特效粒子
    
    [Header("事件监听")] 
    public VoidEventSO imuKickEventSO;
    public VoidEventSO clearColliderEventSO;

    private void OnEnable()
    {
        imuKickEventSO.onEventRaised += Kick;
        clearColliderEventSO.onEventRaised += ClearCollider;
    }

    private void OnDisable()
    {
        imuKickEventSO.onEventRaised -= Kick;
        clearColliderEventSO.onEventRaised -= ClearCollider;
    }

    private void Start()
    {
        checkedLayer = LayerMask.NameToLayer("Ball");
    }
    

    private void OnTriggerEnter(Collider col)
    {
        // 每进入到判定区域就尝试加入到数组
        if (col.gameObject.layer == checkedLayer)
        {
            _colliders.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        // 出范围就删除
        _colliders.Remove(col.gameObject);
    }

    // 把数组中所有的球都踢出去
    private void Kick()
    {
        // Debug.Log(_colliders.Count);
        // Debug.Log("Shoot!");
        if (_colliders is not { Count: > 0 })
        {
            return;
        }
        
        kickParticle.Play();
        
        foreach (var football in _colliders)
        {
            football.gameObject.GetComponent<ShootFootball>().Shoot();
        }
    }

    private void ClearCollider()
    {
        _colliders.Clear();
    }
}
