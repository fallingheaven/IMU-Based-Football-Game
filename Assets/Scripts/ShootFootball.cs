using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShootFootball : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    public bool hit = false;
    // public bool reachable;
    // public Vector3 hitDirection;
    public float hitForce = 15f;
    // public Vector3 bottomOffset;
    // public float checkRadius;
    // public bool onGround;
    // public LayerMask checkedLayer;
    private void Start()
    {
        // hit = false;
        // reachable = false;
    }

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 position)
    {
        Debug.Log("准备射门");
        if (hit)
        {
            return;
        }
        Debug.Log($"成功射门, {(transform.position - position).normalized * hitForce}");
        
        hit = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce((transform.position - position).normalized * hitForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        // Check();
        // Debug.Log(_rigidbody.velocity);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     var position = transform.position;
    //     Gizmos.DrawWireSphere(position + bottomOffset, checkRadius);
    // }
    
    // private void Check()
    // {
    //     var colliders = Physics.OverlapSphere(transform.position + bottomOffset, checkRadius, checkedLayer);
    //     onGround = colliders.Length > 0;
    //     Debug.Log(colliders.Length);
    //     // bool containsGround = colliders.Any(col => col.gameObject.layer == checkedLayer);
    // }
}
