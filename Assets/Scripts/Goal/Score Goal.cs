using UnityEngine;

// 得分判定球门
public class ScoreGoal : MonoBehaviour, IFootballChecker
{
    public FloatEventSO updateScoreEventSO;
    public GameObjectEventSO returnFootballEventSO;
    public LayerMask footballLayer;
    public int addedScore = 1;
    
    private void Start()
    {
        footballLayer = LayerMask.NameToLayer("Ball");
    }
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != footballLayer) return;
        
        updateScoreEventSO.RaiseEvent(addedScore);
        returnFootballEventSO.RaiseEvent(col.gameObject);
    }
}
