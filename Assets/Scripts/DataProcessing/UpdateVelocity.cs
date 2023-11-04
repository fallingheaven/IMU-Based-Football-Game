using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateVelocity : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _acc;
    private Vector3 _initAcc;
    private bool _first;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _acc = new Vector3(0, 0, 0);
        _initAcc = new Vector3(0, 0, 0);
        _first = true;
    }

    private void Update()
    {
        // _rigidbody.velocity += (_acc - _initAcc) * 0.001f * Time.deltaTime;
        Debug.Log($"{_rigidbody.velocity}");
    }

    public void ChangeVelocity(float[] acc)
    {
        _acc = new Vector3(acc[0], acc[1], acc[2]);
        
        if (_first)
        {
            _initAcc = _acc;
            _first = false;
        }
    }
}
