using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("结束游戏");
        Application.Quit();
    }
}
