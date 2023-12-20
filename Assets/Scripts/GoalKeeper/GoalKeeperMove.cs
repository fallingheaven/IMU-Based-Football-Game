using System;
using UnityEngine;

public class GoalKeeperMove : MonoBehaviour
{
    public Vector2 moveRange;
    public float moveVelocity;
    private bool _moveLeft = true;
    private Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        var pos = transform.position;

        transform.position = _moveLeft switch
        {
            true => new Vector3(Mathf.MoveTowards(pos.x, moveRange.x, Time.deltaTime * moveVelocity),
                pos.y, pos.z),
            false => new Vector3(Mathf.MoveTowards(pos.x, moveRange.y, Time.deltaTime * moveVelocity),
                pos.y, pos.z),
        };
        
        if (Math.Abs(pos.x - moveRange.x) < 1e-6)
        {
            _moveLeft = false;
        }
        else if (Math.Abs(pos.x - moveRange.y) < 1e-6)
        {
            _moveLeft = true;
        }
    }
}
