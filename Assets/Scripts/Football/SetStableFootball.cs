using UnityEngine;

// 用于把球静止地放在某个位置
public class SetStableFootball : MonoBehaviour
{
    private CharacterPool _characterPool;
    public Vector3 initPosition;
    private void OnEnable()
    {
        _characterPool = GetComponent<CharacterPool>();
    }

    private void FixedUpdate()
    {
        if (_characterPool.availableNum <= 0) return;
        
        var football = _characterPool.GetCharacterFromPool();
        
        var _rigidbody = football.GetComponent<Rigidbody>();
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        football.transform.position = initPosition;
    }
}
