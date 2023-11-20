using System;
using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    public LayerMask checkedLayer;
    private List<GameObject> _colliders = new List<GameObject>();
    public ParticleSystem kickParticle;
    [Header("事件监听")] 
    public VoidEventSO imuKickEventSO;

    private void OnEnable()
    {
        imuKickEventSO.onEventRaised += Kick;
    }

    private void OnDisable()
    {
        imuKickEventSO.onEventRaised -= Kick;
    }

    private void Start()
    {
        checkedLayer = LayerMask.NameToLayer("Ball");
    }
    

    private void OnTriggerEnter(Collider col)
    {
        // Debug.Log($"{(int)checkedLayer}, {col.gameObject.layer}");
        if (col.gameObject.layer == checkedLayer)
        {
            _colliders.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        _colliders.Remove(col.gameObject);
    }

    public void Kick()
    {
        Debug.Log("Shoot!");
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
}
