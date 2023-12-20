using UnityEngine;
using UnityEngine.Events;

public class SeeFootball : MonoBehaviour
{
    public LayerMask checkedLayer;
    public UnityEvent<Vector3> jump;

    private void OnEnable()
    {
        checkedLayer = LayerMask.NameToLayer("Ball");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == checkedLayer)
        {
            Debug.Log("jump");
            jump.Invoke(col.transform.position);
        }
    }
}
