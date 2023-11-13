using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> sceneLoadEvent;

    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="sceneToLoad"> 要加载的场景 </param>
    /// <param name="positionTogo"> player目标坐标 </param>
    /// <param name="fadeScreen"> 是否渐入渐出 </param>
    public void RaiseSceneLoadEvent(GameSceneSO sceneToLoad, Vector3 positionTogo, bool fadeScreen)
    {
        sceneLoadEvent?.Invoke(sceneToLoad, positionTogo, fadeScreen);
    }
}
