using UnityEngine;

// 用于足球单独判断球门类以外的物体
public class FootballPhyscisCheck : MonoBehaviour
{
    public GameObjectEventSO returnFootballEventSO;
    public LayerMask missLayer;
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == missLayer)
        {
            returnFootballEventSO.RaiseEvent(col.gameObject);
        }
    }
}
