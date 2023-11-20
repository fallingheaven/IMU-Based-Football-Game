using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")] 
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO mainMenu;

    [Header("场景加载")]
    private GameSceneSO _currentScene;
    private GameSceneSO _sceneToLoad;
    private Vector3 _positionToGo;
    private bool _fadeScreen;

    public float fadeDuration;
    public GameObject player;

    [Header("加载界面")]
    public Slider slider;
    public Text text;
    public UpdateLoadSlider updateLoadSlider;
    public GameObject loadSceneSlider;

    private void Awake()
    {
        _currentScene = mainMenu;
        mainMenu.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        player.transform.position =new Vector3(0, 1, 0);
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
        // Debug.Log($"加载场景, {sceneToLoad.sceneReference.SubObjectName}");

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
        
        var operation = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);

        SceneManager.sceneLoaded += OnSceneLoaded;
            
        StartCoroutine(StartLoadSceneSlider(operation));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if (scene.name != _sceneToLoad.sceneReference.editorAsset.name) return;
        Debug.Log("加载完成");
        
        // 取消订阅事件，以防止多次调用
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // 激活加载的场景
        SetSceneActive(scene.name);

        // 隐藏加载界面
        loadSceneSlider.SetActive(false);

        player.transform.position = _sceneToLoad.initialPosition;
    }

    private void SetSceneActive(string sceneName)
    {
        // Debug.Log(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    private IEnumerator StartLoadSceneSlider(AsyncOperationHandle<SceneInstance> operation)
    {
        // Debug.Log("开始加载场景");
        loadSceneSlider.SetActive(true);
        
        while (!operation.IsDone)
        {
            updateLoadSlider.SetValue(operation.PercentComplete);
            // Debug.Log(operation.PercentComplete);

            yield return null;
        }
        
        updateLoadSlider.SetValue(1f);
        // while (slider.value < 1f)
        // {
        //     Debug.Log(slider.value);
        //     // if(slider.value >= 1f) yield break;
        //     yield return null;
        // }
    }
}
