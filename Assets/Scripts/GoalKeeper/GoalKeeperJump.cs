using System;
using UnityEngine;

public class GoalKeeperJump : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump(Vector3 target)
    {
        _rigidbody.velocity = Vector3.zero;
        
        var force = new Vector3(target.x, target.y + 2f, 0) - transform.position;
        force.z = 0f;
        force.x /= (float)Math.Pow(Math.Pow(target.x, 2) + Math.Pow(target.y, 2), 0.5f);
        force.y /= (float)Math.Pow(Math.Pow(target.x, 2) + Math.Pow(target.y, 2), 0.5f);
        
        _rigidbody.AddForce(force * jumpForce, ForceMode.Impulse);
    }
}
