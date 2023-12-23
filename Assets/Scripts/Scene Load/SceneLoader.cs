using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject StartInfo;
    private bool _isMainMenu;
    
    [Header("事件监听")] 
        
        public SceneLoadEventSO sceneLoadEventSO;
        public GameSceneSO mainMenu; // 一开始要显示的主菜单

    [Header("场景加载")] 
        public Animator transition;
        public float fadeDuration; // 淡入淡出持续时间
        public GameObject player;
        private GameSceneSO _currentScene; // 当前的场景
        private GameSceneSO _sceneToLoad; // 要加载的场景
        private bool _fadeScreen; // 是否淡入淡出
        
        private AsyncOperationHandle<SceneInstance> _loadHandle;

        public VoidEventSO clearColliderEventSO; // 在场景加载完才能再清空判定区
    
    [Header("加载界面")] 
    
        public UpdateLoadSlider updateLoadSlider; // 加载的条
        public GameObject loadSceneSlider;
        public VoidEventSO newGameEventSO;
        // public VoidEventSO nextLevelEventSO;
        
    private void Awake()
    {
        // 场景初始化加载主菜单
        _currentScene = mainMenu;
        _loadHandle = mainMenu.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
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
        _isMainMenu = sceneToLoad.sceneType == SceneType.Menu;
        StartInfo.SetActive(false);
        
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
        clearColliderEventSO.RaiseEvent();
        
        if (_fadeScreen)
        {
            transition.SetTrigger("Start");
        }
        
        yield return new WaitForSeconds(fadeDuration);
        
        loadSceneSlider.SetActive(true);
        yield return Addressables.UnloadSceneAsync(_loadHandle);
        // yield return _currentScene.sceneReference.UnLoadScene();
        
        // 本来想做先加载场景，等进度条满了再激活场景，但不知为何如果写了false，就加载不成功了，只好写true了
        // 后注：完成了！
        _loadHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, false);

        // _loadHandle.Completed += OnSceneLoaded;
        // SceneManager.sceneLoaded += OnSceneLoaded;

        StartCoroutine(StartLoadSceneSlider(_loadHandle));
    }
    
    // 加载进度条
    private IEnumerator StartLoadSceneSlider(AsyncOperationHandle<SceneInstance> loadHandle)
    {
        // Debug.Log("开始加载场景");
        
        while (!loadHandle.IsDone)
        {
            updateLoadSlider.SetValue(loadHandle.PercentComplete);
            // Debug.Log(loadHandle.PercentComplete);
        
            yield return null;
        }
        
        updateLoadSlider.SetValue(1f);
        while (updateLoadSlider.slider.value < 1f)
        {
            yield return null;
        }

        StartCoroutine(OnSceneLoaded(_loadHandle));
    }
    
    // 加载完成场景后关闭加载条，设置玩家初始位置
    private IEnumerator OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
     {
         transition.SetTrigger("End");
         StartInfo.SetActive(_isMainMenu);
         
         // if (scene.name != _sceneToLoad.sceneReference.editorAsset.name) return;
         // Debug.Log("加载完成");
         
         // 取消订阅事件，以防止多次调用
         // handle.Completed -= OnSceneLoaded;
         // SceneManager.sceneLoaded -= OnSceneLoaded;
 
         // 激活加载的场景
         yield return _loadHandle.Result.ActivateAsync();
         // SetSceneActive(scene.name);
 
         // 隐藏加载界面
         loadSceneSlider.SetActive(false);
 
         player.transform.position = _sceneToLoad.initialPosition;
         
         clearColliderEventSO.RaiseEvent();
         newGameEventSO.RaiseEvent();
         // nextLevelEventSO.RaiseEvent();
         
         yield return null;
     }
}
