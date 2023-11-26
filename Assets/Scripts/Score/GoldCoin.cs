using UnityEngine;
using Random = UnityEngine.Random;

public class GoldCoin : MonoBehaviour
{
    public LayerMask footballLayer;
    public FloatEventSO scoreChangeEventSO;
    public float coinScore;
    public Vector3 leftTopPoint, rightBottomPoint;
    public Vector3 center;
    
    private void OnEnable()
    {
        ResetPosition();
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        // if (col.gameObject.layer != footballLayer) return;
        
        scoreChangeEventSO.RaiseEvent(coinScore);
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(center.x + leftTopPoint.x, center.x + rightBottomPoint.x),
                                         Random.Range(center.y + leftTopPoint.y, center.y + rightBottomPoint.y),
                                         Random.Range(center.z + leftTopPoint.z, center.z + rightBottomPoint.z));
        // Debug.Log(transform.position);
        // Debug.Log("重置金币位置");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(center + leftTopPoint, 1);
        Gizmos.DrawSphere(center + rightBottomPoint, 1);
    }
}