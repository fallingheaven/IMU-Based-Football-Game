using UnityEngine;

// 用于把球静止地放在某个位置
public class SetStableFootball : MonoBehaviour
{
    private CharacterPool _characterPool;
    public Vector3 initPosition;
    private void Start()
    {
        _characterPool = GetComponent<CharacterPool>();
    }

    private void FixedUpdate()
    {
        if (_characterPool.availableNum <= 0) return;
        
        var football = _characterPool.GetCharacterFromPool();
        football.transform.position = initPosition;
    }
}
