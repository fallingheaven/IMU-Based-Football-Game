using UnityEngine;
using Random = UnityEngine.Random;

public class CinemaShakeManager : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO CameraShakeEventSO;
    
    private Vector3 _originalPosition; // 摄像机初始位置， 用于恢复
    private Quaternion _originalRotation; // 摄像机初始角度，用于恢复

    private float _seed; // 震动随机种子
    private float _trauma = 0; // 当前抖动力度
    private float _maximumStress = 0.4f; // 最大抖动力度
    private float _frequency = 15; // 抖动频率
    private Vector3 _maximumTranslationShake = Vector3.one * 0.5f; // 最大位移抖动
    private Vector3 _maximumAngularShake = Vector3.one * 2; // 醉倒角度抖动
    private float _traumaExponent = 2; // 二次方衰减指数
    private float _recoverySpeed = 1.0f; // 恢复速度（系数）
    
    private void OnEnable()
    {
        CameraShakeEventSO.onEventRaised += ShakeCinema;
    }

    private void OnDisable()
    {
        CameraShakeEventSO.onEventRaised -= ShakeCinema;
    }

    private void Awake()
    {
        var _transform = transform;
        _originalPosition = _transform.position;
        _originalRotation = _transform.rotation;
        _seed = Random.value;
    }

    private void FixedUpdate()
    {
        var shake = Mathf.Pow(_trauma, _traumaExponent);

        if (shake < 0.01f)
        {
            var _transform = transform;
            _transform.position = _originalPosition;
            _transform.rotation = _originalRotation;
            return;
        }

        var translationShake = new Vector3(
            _maximumTranslationShake.x * (Mathf.PerlinNoise(_seed, Time.time * _frequency) * 2 - 1),
            _maximumTranslationShake.y * (Mathf.PerlinNoise(_seed + 1, Time.time * _frequency) * 2 - 1),
            _maximumTranslationShake.z * (Mathf.PerlinNoise(_seed + 2, Time.time * _frequency) * 2 - 1)
        ) * shake;
        
        var angularShake = new Vector3(
            _maximumAngularShake.x * (Mathf.PerlinNoise(_seed + 3, Time.time * _frequency) * 2 - 1),
            _maximumAngularShake.y * (Mathf.PerlinNoise(_seed + 4, Time.time * _frequency) * 2 - 1),
            _maximumAngularShake.z * (Mathf.PerlinNoise(_seed + 5, Time.time * _frequency) * 2 - 1)
        ) * shake;
        
        var Transform = transform;
        Transform.localPosition = _originalPosition + translationShake * shake;
        Transform.localRotation = Quaternion.Euler(_originalRotation.eulerAngles + angularShake);
        
        _trauma = Mathf.Clamp01(_trauma - _recoverySpeed * Time.deltaTime);
    }

    private void ShakeCinema()
    {
        _trauma = Mathf.Clamp01(_maximumStress);
    }
}
