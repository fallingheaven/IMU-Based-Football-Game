using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeFootball : MonoBehaviour
{
    public float serveTimeGap;
    private float _currentTimeGap;
    public bool serving;
    private CharacterPool _characterPool;

    public Vector3 initPosition;
    public Vector3 serveForce;
    private void Awake()
    {
        // serving = false;
    }

    private void Start()
    {
        _characterPool = GetComponent<CharacterPool>();
    }

    private void FixedUpdate()
    {
        switch (serving)
        {
            case false when _currentTimeGap <= 0:
            {
                _currentTimeGap = serveTimeGap;
                break;
            }
            case true when _currentTimeGap > 0:
            {
                _currentTimeGap -= Time.deltaTime;
                break;
            }
            case true when _currentTimeGap <= 0:
            {
                var football = _characterPool.GetCharacterFromPool();
                if (football == null)
                {
                    break;
                }
                
                football.transform.position = initPosition;
                football.GetComponent<ShootFootball>().hit = false;
                Serve(football);

                _currentTimeGap = serveTimeGap;
                
                break;
            }
        }
    }

    private void Serve(GameObject football)
    {
        var _rigidbody = football.GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(serveForce, ForceMode.Impulse);
    }
}
