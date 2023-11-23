using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, bool> sceneLoadEvent;

    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="sceneToLoad"> 要加载的场景 </param>
    /// <param name="fadeScreen"> 是否渐入渐出 </param>
    public void RaiseSceneLoadEvent(GameSceneSO sceneToLoad, bool fadeScreen)
    {
        sceneLoadEvent?.Invoke(sceneToLoad, fadeScreen);
    }
}
