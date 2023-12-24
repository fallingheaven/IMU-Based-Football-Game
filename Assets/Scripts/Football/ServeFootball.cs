using System;
using UnityEngine;
using Random = UnityEngine.Random;

// 发球器
public class ServeFootball : MonoBehaviour
{
    public float bombChance;
    
    public float serveTimeGap; // 发球间隔
    private float _currentTimeGap; // 当前间隔剩余
    public bool serving = false; // 发球状态
    public CharacterPool footballPool;
    public CharacterPool bombPool;
    
    public Vector3 initPosition; // 发球位置
    public Vector3 serveVelocity; // 发球速度矢量
    public float absoluteVelocity; // 发球速度大小
    public GameObject player;
    
    private bool _pause = false;

    [Header("事件")] 
    public GameObjectFloatFloatEventSO delayedReturnFootballEventSO;
    
    [Header("事件监听")] 
    public VoidEventSO nextLevelEventSO;
    public VoidEventSO gameOverEventSO;
    public VoidEventSO gameSuccessEventSO;
    public VoidEventSO pauseGameEventSO;
    public VoidEventSO resumeGameEventSO;

    private void OnEnable()
    {
        nextLevelEventSO.onEventRaised += StopServe;
        gameOverEventSO.onEventRaised += StopServe;
        gameSuccessEventSO.onEventRaised += StopServe;
        pauseGameEventSO.onEventRaised += PauseReceive;
        resumeGameEventSO.onEventRaised += ResumeReceive;
    }

    private void OnDisable()
    {
        nextLevelEventSO.onEventRaised -= StopServe;
        gameOverEventSO.onEventRaised -= StopServe;
        gameSuccessEventSO.onEventRaised -= StopServe;
        pauseGameEventSO.onEventRaised -= PauseReceive;
        resumeGameEventSO.onEventRaised -= ResumeReceive;
    }

    private void Start()
    {
        // footballPool = GetComponent<CharacterPool>();
        player = GameObject.Find("Kick Checker");
        SetServeVelocity();
    }

    // 用于将球准确地踢到player判定区域里
    private void SetServeVelocity()
    {
        var playerPosition = player.transform.position;
        serveVelocity = new Vector3(playerPosition.x - initPosition.x, 0, playerPosition.z - initPosition.z).normalized;
        serveVelocity *= absoluteVelocity;
        
        var estimatedTime = (playerPosition.x - initPosition.x) / serveVelocity.x;

        serveVelocity.y = 9.81f * estimatedTime / 2 + 0.5f;
    }

    private void FixedUpdate()
    {
        if (_pause) return;
        
        switch (serving)
        {
            case false when _currentTimeGap <= 0:
            {
                _currentTimeGap = serveTimeGap;
                break;
            }
            case true when _currentTimeGap > 0:
            {
                _currentTimeGap -= Time.deltaTime;
                break;
            }
            case true when _currentTimeGap <= 0:
            {
                if (footballPool.availableNum <= 0) break;
                
                // 如果随机到炸弹
                if (Random.Range(0, 1f) <= bombChance)
                {
                    var tmpBomb = bombPool.GetCharacterFromPool();
                    tmpBomb.transform.position = initPosition;
                    Serve(tmpBomb);

                    _currentTimeGap = serveTimeGap;
                    break;
                }
                
                // 取一个可用的球发射出去
                var football = footballPool.GetCharacterFromPool();
                football.transform.position = initPosition;
                football.GetComponent<ShootFootball>().hit = false;
                Serve(football);
                
                delayedReturnFootballEventSO.RaiseEvent(football.gameObject, 10f);
        
                _currentTimeGap = serveTimeGap;
                
                break;
            }
        }
    }
    
    // 发射
    private void Serve(GameObject football)
    {
        var _rigidbody = football.GetComponent<Rigidbody>();
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = serveVelocity;
    }

    public void StartServe()
    {
        serving = true;
    }

    private void StopServe()
    {
        serving = false;
    }
    
    private void PauseReceive()
    {
        _pause = true;
    }
    
    private void ResumeReceive()
    {
        _pause = false;
    }
}
