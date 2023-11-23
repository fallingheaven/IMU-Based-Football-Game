using UnityEngine;

// miss判定球门（也可以不是球门样式）
public class MissGoal : MonoBehaviour, IFootballChecker
{
    public GameObjectEventSO returnFootballEventSO;
    public LayerMask footballLayer;
    
    private void Start()
    {
        footballLayer = LayerMask.NameToLayer("Ball");
    }
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != footballLayer) return;
        
        returnFootballEventSO.RaiseEvent(col.gameObject);
    }
}
