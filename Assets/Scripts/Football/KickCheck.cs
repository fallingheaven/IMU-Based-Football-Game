using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    // public float checkRadius = 1f;
    public LayerMask checkedLayer;
    private List<GameObject> _colliders = new List<GameObject>();
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

    private void FixedUpdate()
    {
        // Physics.OverlapSphereNonAlloc(transform.position, checkRadius, _colliders, checkedLayer);
        // Debug.Log($"判定范围里有{_colliders.Count}个球");
    }

    public void Kick()
    {
        Debug.Log("Shoot!");
        if (_colliders is not { Count: > 0 })
        {
            return;
        }
        
        foreach (var football in _colliders)
        {
            football.gameObject.GetComponent<ShootFootball>().Shoot();
        }
    }
}
