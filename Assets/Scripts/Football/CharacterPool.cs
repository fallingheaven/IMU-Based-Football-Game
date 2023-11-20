using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterPool : MonoBehaviour
{
    public GameObject characterPrefab;
    public int poolSize ;
    private int _availableNum;
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
        _availableNum = poolSize;
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

    public GameObject GetCharacterFromPool()
    {
        if (_availableNum <= 0)
        {
            return null;
        }
        
        foreach (var character in _characterPool.Where(character => !character.activeInHierarchy))
        {
            character.SetActive(true);
            _availableNum--;
            return character;
        }

        return null;
    }

    private void ReturnCharacterToPool(GameObject character)
    {
        _availableNum++;
        character.GetComponent<Rigidbody>().velocity = Vector3.zero;
        character.transform.position = Vector3.zero;
        character.SetActive(false);
    }
}