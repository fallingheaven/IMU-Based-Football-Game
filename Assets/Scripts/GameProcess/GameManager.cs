using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTarget gameTarget;
    public ScoreDataSO scoreData;
    // public SceneLoadEventSO sceneLoadEventSO;
    public VoidEventSO startLevelEventSO;
    public VoidEventSO gameOverEventSO;
    public VoidEventSO gameSuccessEventSO;
    public VoidEventSO nextLevelEventSO;
    public FloatFloatEventSO updateLevelTimeCounterEventSO;
    public float levelTime;

    [Header("事件监听")]
    public VoidEventSO newGameEventSO;
    public VoidEventSO pauseGameEventSO;
    
    [Header("场景返回")]
    private readonly TimeCounter _levelTimeCounter = new TimeCounter();
    
    private readonly Dictionary<Difficulty, float> _difficultyGrowDegree = new Dictionary<Difficulty, float>()
    {
        { Difficulty.Easy   , 1.01f }, 
        { Difficulty.Normal , 1.05f }, 
        { Difficulty.Hard   , 1.08f  }, 
        { Difficulty.Crazy  , 1.10f  }
    };
    
    private readonly Dictionary<Difficulty, float> _levelLength = new Dictionary<Difficulty, float>()
    {
        { Difficulty.Easy   , 10f }, 
        { Difficulty.Normal , 15f }, 
        { Difficulty.Hard   , 20f }, 
        { Difficulty.Crazy  , 30f }
    };

    private void OnEnable()
    {
        // nextLevelEventSO.onEventRaised += StartNextLevel;
        newGameEventSO.onEventRaised += NewGame;
        pauseGameEventSO.onEventRaised += _levelTimeCounter.Pause;
        // newGameEventSO.onEventRaised += StartNextLevel;

    }

    private void OnDisable()
    {
        // nextLevelEventSO.onEventRaised -= StartNextLevel;
        newGameEventSO.onEventRaised -= NewGame;
        pauseGameEventSO.onEventRaised -= _levelTimeCounter.Pause;
        // newGameEventSO.onEventRaised -= StartNextLevel;
    }

    private void FixedUpdate()
    {
        // Debug.Log($"{_levelTimeCounter._currentTime}, {scoreData.currentScore}, {gameTarget.currentTarget}");;
        if (_levelTimeCounter.End()) return;
        
        _levelTimeCounter.FixedUpdate();
        updateLevelTimeCounterEventSO.RaiseEvent(_levelTimeCounter.GetCurrentTime(), _levelTimeCounter.GetTotalTime());
        
        if (!_levelTimeCounter.End()) return;
        
        if (scoreData.currentScore < gameTarget.currentTarget)
        {
            // Debug.Log("游戏结束");
            // ResetLevel();
            // sceneLoadEventSO.RaiseSceneLoadEvent(mainMenu, true);
            gameOverEventSO.RaiseEvent();
        }

        else if (scoreData.currentScore >= gameTarget.currentTarget)
        {
            // Debug.Log("下一关");
            if (Math.Abs(gameTarget.currentLevel - _levelLength[gameTarget.difficultyDegree]) < 1e-6)
            {
                gameSuccessEventSO.RaiseEvent();
                return;
            }
            
            StartNextLevel();
            nextLevelEventSO.RaiseEvent();
        }
    }
    
    private void NewGame()
    {
        ResetLevel();
        StartNextLevel();
        // Debug.Log(gameTarget.currentLevel);
    }
    
    private IEnumerator NextLevelTransition()
    {
        // transition.SetTrigger("Start");
        // yield return new WaitForSeconds(1f);
        // transition.SetTrigger("End");
        
        startLevelEventSO.RaiseEvent();
        yield return new WaitForSeconds(1.5f);// 为了显示关卡信息的时间间隔
        _levelTimeCounter.StartTimeCount(levelTime);
        
        yield return null;
    }
    
    private void StartNextLevel()
    {
        // Debug.Log("开始下一关");
        gameTarget.currentLevel++;
        gameTarget.currentTarget += gameTarget.initialTarget *(float)
                                   Math.Pow(_difficultyGrowDegree[gameTarget.difficultyDegree],
                                   gameTarget.currentLevel);
        
        StartCoroutine(NextLevelTransition());
    }
    
    private void ResetLevel()
    {
        gameTarget.currentLevel = 0f;
        gameTarget.currentTarget = 0f;
        scoreData.currentScore = 0f;
    }

    public void ChangeLevelDifficulty(Difficulty difficulty)
    {
        gameTarget.difficultyDegree = difficulty;
    }
    
    public void EndGame()
    {
        // Debug.Log("结束游戏");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
