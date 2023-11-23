using UnityEngine;

// 临时脚本
public class Teleport : MonoBehaviour
{
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO sceneToLoad;
    
    // public Vector3 positionToGo;
    // private TimeCounter _timeCounter;

    public void TeleportAction()
    {
        // if (sceneToLoad.sceneType == SceneType.GameScene)
        // {
        //     _timeCounter.StartTimeCount(3);
        // }
        
        sceneLoadEventSO.RaiseSceneLoadEvent(sceneToLoad, true);
    }
}
