using UnityEngine;

public class GoalKeeperMove : MonoBehaviour
{
    public LayerMask groundLayer;
    public Vector2 moveRange;
    public float moveVelocity;
    private bool _moveLeft = true;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer == groundLayer)
        {
            Move();
        }
    }

    private void Move()
    {
        var pos = transform.position;
        
        transform.position = _moveLeft switch
        {
            true => new Vector3(Mathf.MoveTowards(pos.x, moveRange.x - 0.5f, Time.deltaTime * moveVelocity),
                pos.y, pos.z),
            false => new Vector3(Mathf.MoveTowards(pos.x, moveRange.y + 0.5f, Time.deltaTime * moveVelocity),
                pos.y, pos.z),
        };
        
        if (pos.x - moveRange.x < 0)
        {
            _moveLeft = false;
        }
        else if (pos.x - moveRange.y > 0)
        {
            _moveLeft = true;
        }
    }
}
