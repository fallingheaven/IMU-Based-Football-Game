using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameTarget gameTarget;
    public ScoreDataSO scoreData;
    public SceneLoadEventSO sceneLoadEventSO;
    public VoidEventSO startLevelEventSO;

    [Header("事件监听")]
    public VoidEventSO nextLevelEventSO;
    public VoidEventSO newGameEventSO;
    
    [Header("场景返回")]
    public GameSceneSO mainMenu;
    public Animator transition;
    private readonly TimeCounter _levelTimeCounter = new TimeCounter();
    
    private readonly Dictionary<Difficulty, float> _difficultyGrowDegree = new Dictionary<Difficulty, float>()
    {
        { Difficulty.Easy   , 1.01f }, 
        { Difficulty.Normal , 1.05f }, 
        { Difficulty.Hard   , 1.1f  }, 
        { Difficulty.Crazy  , 1.2f  }
    };

    private void OnEnable()
    {
        // nextLevelEventSO.onEventRaised += StartNextLevel;
        newGameEventSO.onEventRaised += StartNextLevel;
        newGameEventSO.onEventRaised += ResetLevel;
    }
    
    private void OnDisable()
    {
        // nextLevelEventSO.onEventRaised -= StartNextLevel;
        newGameEventSO.onEventRaised -= StartNextLevel;
        newGameEventSO.onEventRaised -= ResetLevel;
    }

    private void FixedUpdate()
    {
        // Debug.Log($"{_levelTimeCounter._currentTime}, {scoreData.currentScore}, {gameTarget.currentTarget}");;
        if (_levelTimeCounter.End()) return;
        
        _levelTimeCounter.FixedUpdate();
        
        if (!_levelTimeCounter.End()) return;
        
        if (scoreData.currentScore < gameTarget.currentTarget)
        {
            Debug.Log("游戏结束");
            ResetLevel();
            sceneLoadEventSO.RaiseSceneLoadEvent(mainMenu, true);
        }

        else if (scoreData.currentScore >= gameTarget.currentTarget)
        {
            Debug.Log("下一关");

            StartNextLevel();
            nextLevelEventSO.RaiseEvent();
        }
    }

    private IEnumerator NextLevelTransition()
    {
        // transition.SetTrigger("Start");
        // yield return new WaitForSeconds(1f);
        // transition.SetTrigger("End");
        
        _levelTimeCounter.StartTimeCount(10f);
        startLevelEventSO.RaiseEvent();
        yield return null;
    }
    
    private void StartNextLevel()
    {
        // Debug.Log("开始下一关");
        StartCoroutine(NextLevelTransition());
        
        gameTarget.currentLevel++;
        gameTarget.currentTarget += gameTarget.initialTarget *(float)
                                   Math.Pow(_difficultyGrowDegree[gameTarget.difficultyDegree],
                                   gameTarget.currentLevel);
    }
    
    private void ResetLevel()
    {
        gameTarget.currentLevel = 0f;
        gameTarget.currentTarget = gameTarget.initialTarget;
        scoreData.currentScore = 0f;
    }

    public void ChangeLevelDifficulty(Difficulty difficulty)
    {
        gameTarget.difficultyDegree = difficulty;
    }
    
    public void EndGame()
    {
        Debug.Log("结束游戏");
        Application.Quit();
    }
}
