using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")] 
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO mainMenu; // 一开始要显示的主菜单

    [Header("场景加载")]
    private GameSceneSO _currentScene; // 当前的场景
    private GameSceneSO _sceneToLoad; // 要加载的场景
    private bool _fadeScreen; // 是否淡入淡出（还没做）
    public float fadeDuration; // 淡入淡出持续时间
    public GameObject player;

    [Header("加载界面")]
    public UpdateLoadSlider updateLoadSlider; // 加载的条
    public GameObject loadSceneSlider;

    private void Awake()
    {
        // 场景初始化加载主菜单
        _currentScene = mainMenu;
        mainMenu.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        player.transform.position = mainMenu.initialPosition;
    }

    private void OnEnable()
    {
        sceneLoadEventSO.sceneLoadEvent += OnLoadEvent;
    }

    private void OnDisable()
    {
        sceneLoadEventSO.sceneLoadEvent -= OnLoadEvent;
    }

    private void OnLoadEvent(GameSceneSO sceneToLoad, bool fadeScreen)
    {
        (_sceneToLoad, _fadeScreen) = (sceneToLoad, fadeScreen);

        if (_sceneToLoad == null)
        {
            return;
        }
        
        StartCoroutine(UnloadCurrentScene());
    }

    // 先卸载当前场景再加载新场景
    private IEnumerator UnloadCurrentScene()
    {
        if (_fadeScreen)
        {
            //TODO: 场景渐入渐出
        }

        yield return new WaitForSeconds(fadeDuration);
        
        yield return _currentScene.sceneReference.UnLoadScene();
        
        // 本来想做先加载场景，等进度条满了再激活场景，但不知为何如果写了false，就加载不成功了，只好写true了
        var operation = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);

        SceneManager.sceneLoaded += OnSceneLoaded;
            
        StartCoroutine(StartLoadSceneSlider(operation));
    }

    // 加载完成场景后关闭加载条，设置玩家初始位置
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if (scene.name != _sceneToLoad.sceneReference.editorAsset.name) return;
        // Debug.Log("加载完成");
        
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

    // 加载进度条
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
