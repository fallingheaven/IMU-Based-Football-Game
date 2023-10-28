using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootFootball : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    public Vector3 bottomOffset;
    public float checkRadius;
    public bool onGround;
    public LayerMask checkedLayer;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        if (!onGround) return;
        _rigidbody.AddForce(new Vector3(0, 1.5f, 0.5f), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Check();
    }

    private void OnDrawGizmosSelected()
    {
        var position = transform.position;
        Gizmos.DrawWireSphere(position + bottomOffset, checkRadius);
    }
    
    private void Check()
    {
        var colliders = Physics.OverlapSphere(transform.position + bottomOffset, checkRadius, checkedLayer);
        onGround = colliders.Length > 0;
        Debug.Log(colliders.Length);
        // bool containsGround = colliders.Any(col => col.gameObject.layer == checkedLayer);
    }
}
