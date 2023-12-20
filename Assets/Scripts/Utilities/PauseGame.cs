using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public VoidEventSO pauseGameEventSO;
    public VoidEventSO resumeGameEventSO;
    
    public void Pause()
    {
        // Time.timeScale = 0f;
        pauseGameEventSO.RaiseEvent();
    }

    public void Resume()
    {
        // Time.timeScale = 1f;
        resumeGameEventSO.RaiseEvent();
    }
}
