using UnityEngine;

// 传送判定球门
public class TeleportGoal : MonoBehaviour, IFootballChecker
{
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO sceneToLoad;
    public GameObjectEventSO returnFootballEventSO;
    public LayerMask footballLayer;
    
    private void Start()
    {
        footballLayer = LayerMask.NameToLayer("Ball");
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != footballLayer) return;
        
        sceneLoadEventSO.RaiseSceneLoadEvent(sceneToLoad, true);
        returnFootballEventSO.RaiseEvent(col.gameObject);
    }
}
