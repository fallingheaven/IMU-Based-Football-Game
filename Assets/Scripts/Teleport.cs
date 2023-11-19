using System;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO sceneToLoad;
    public Vector3 positionToGo;
    private TimeCounter _timeCounter;

    private void Start()
    {
        _timeCounter = GameObject.Find("Time Counter").GetComponent<TimeCounter>();
    }

    public void TeleportAction()
    {
        // Debug.Log($"场景传送，到{sceneToLoad.sceneReference.editorAsset.name}");

        if (sceneToLoad.sceneType == SceneType.GameScene)
        {
            _timeCounter.StartTimeCount(3);
        }
        
        sceneLoadEventSO.RaiseSceneLoadEvent(sceneToLoad, positionToGo, true);
    }
}
