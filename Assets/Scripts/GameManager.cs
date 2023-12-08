using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTarget gameTarget;
    
    public void EndGame()
    {
        Debug.Log("结束游戏");
        Application.Quit();
    }
}
