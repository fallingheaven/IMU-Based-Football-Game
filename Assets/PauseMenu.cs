using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    
    public void OpenPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
    }
}
