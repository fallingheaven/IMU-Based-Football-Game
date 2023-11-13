using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")] 
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO mainMenu;

    private GameSceneSO _currentScene;
    private GameSceneSO _sceneToLoad;
    private Vector3 _positionToGo;
    private bool _fadeScreen;

    public float fadeDuration;

    private void Awake()
    {
        _currentScene = mainMenu;
        mainMenu.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        sceneLoadEventSO.sceneLoadEvent += OnLoadEvent;
    }

    private void OnDisable()
    {
        sceneLoadEventSO.sceneLoadEvent -= OnLoadEvent;
    }

    private void OnLoadEvent(GameSceneSO sceneToLoad, Vector3 positionToGo, bool fadeScreen)
    {
        (_sceneToLoad, _positionToGo, _fadeScreen) = (sceneToLoad, positionToGo, fadeScreen);
        Debug.Log($"加载场景, {sceneToLoad.sceneReference.SubObjectName}");

        if (_sceneToLoad == null)
        {
            return;
        }
        
        StartCoroutine(UnloadCurrentScene());
    }

    private IEnumerator UnloadCurrentScene()
    {
        if (_fadeScreen)
        {
            //TODO: 场景渐入渐出
        }

        yield return new WaitForSeconds(fadeDuration);
        
        yield return _currentScene.sceneReference.UnLoadScene();
        
        _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }
}
