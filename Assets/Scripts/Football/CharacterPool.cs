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
    public GameObjectEventSO ReturnFootballEventSO;
    
    private readonly List<GameObject> _characterPool = new List<GameObject>();

    private void OnEnable()
    {
        ReturnFootballEventSO.OnEventRaised += ReturnCharacterToPool;
    }

    private void OnDisable()
    {
        ReturnFootballEventSO.OnEventRaised -= ReturnCharacterToPool;
    }

    private void Start()
    {
        // 实例化池子，放在父物体（本物体）下
        availableNum = poolSize;
        for (var i = 0; i < poolSize; i++)
        {
            var character = Instantiate(characterPrefab, transform, true);
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
        availableNum++;
        character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        character.transform.position = Vector3.zero;
        character.SetActive(false);
    }
}