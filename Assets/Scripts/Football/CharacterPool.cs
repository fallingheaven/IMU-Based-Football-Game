using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// 角色池
public class CharacterPool : MonoBehaviour
{
    public GameObject characterPrefab; // 需要生成的对象
    public int poolSize; // 池子大小
    public int availableNum; // 当前可用数量，用于判断
    
    [Header("事件监听")]
    public GameObjectEventSO ReturnCharacterEventSO;
    public VoidEventSO nextLevelEventSO;
    public VoidEventSO pauseGameEventSO;
    
    private readonly List<GameObject> _characterPool = new List<GameObject>();

    private void OnEnable()
    {
        ReturnCharacterEventSO.OnEventRaised += ReturnCharacterToPool;
        nextLevelEventSO.onEventRaised += NextLevel;
        pauseGameEventSO.onEventRaised += ReturnAllCharacter;
        
        Init();
    }

    private void OnDisable()
    {
        ReturnCharacterEventSO.OnEventRaised -= ReturnCharacterToPool;
        nextLevelEventSO.onEventRaised -= NextLevel;
        pauseGameEventSO.onEventRaised -= ReturnAllCharacter;
    }

    private void Init()
    {
        // 实例化池子，放在父物体（本物体）下
        availableNum = poolSize;
        for (var i = 0; i < poolSize; i++)
        {
            var character = Instantiate(characterPrefab, transform, false);
            character.name = characterPrefab.name;
            character.SetActive(false);
            _characterPool.Add(character);
            // Debug.Log("实例化完成");
        }
        // Debug.Log("实例化全部完成");
    }
    
    // 从池子里取对象
    public GameObject GetCharacterFromPool()
    {
        if (availableNum <= 0)
        {
            return null;
        }
        
        // 找未激活的
        foreach (var character in _characterPool.Where(character => !character.activeInHierarchy))
        {
            character.SetActive(true);
            availableNum--;
            return character;
        }

        return null;
    }

    // 退回池子
    private void ReturnCharacterToPool(GameObject character)
    {
        if (character == null) return;
        
        availableNum++;
        character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        character.transform.position = Vector3.zero;
        character.SetActive(false);
    }

    private void NextLevel()
    {
        foreach (var football in _characterPool)
        {
            Destroy(football.gameObject);
        }
        
        _characterPool.Clear();
        
        Init();
    }

    private void ReturnAllCharacter()
    {
        foreach (var character in _characterPool)
        {
            ReturnCharacterToPool(character);
        }
    }
}