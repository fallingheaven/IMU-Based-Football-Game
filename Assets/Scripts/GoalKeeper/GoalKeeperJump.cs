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
        var force = new Vector3(target.x, target.y, 0) - transform.position;
        force.z = 0f;
        force.x /= force.sqrMagnitude;
        force.y /= force.sqrMagnitude;
        
        _rigidbody.AddForce(force * jumpForce, ForceMode.Impulse);
    }
}
