using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    public SceneLoadEventSO sceneLoadEventSO;
    public GameSceneSO mainMenu;

    public void BackToTheMainMenu()
    {
        sceneLoadEventSO.RaiseSceneLoadEvent(mainMenu, true);
    }
}
