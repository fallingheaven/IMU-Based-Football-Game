using System.Collections;
using UnityEngine;

public class ShootFootball : MonoBehaviour
{
    [Header("事件监听")] 
    public TransformEventSO getCameraTransfromEventSO; // 接收摄像机Transform，用于获取其旋转矩阵
    
    private Rigidbody _rigidbody;
    private AudioDefinition _kickAudio; // 踢球时的音效组
    private CinemaShake _shakeCinema; // 摄像机震动代码
    private UnityPythonCommunication _communication; // 和imu互通传数据的
    
    public bool hit = false; // 暂时没用
    private Vector3 _hitDirection; // 踢球方向
    public float hitForce = 15f; // 踢球力度
    public float hitPauseTime; // 踢球时的时停，增加打击感

    private Transform _cameraTransform;
    private Quaternion _cameraQuaternion;

    private void OnEnable()
    {
        _communication = GameObject.Find("Communication").GetComponent<UnityPythonCommunication>();
        _rigidbody = GetComponent<Rigidbody>();
        _kickAudio = GetComponent<AudioDefinition>();
        _shakeCinema = GetComponent<CinemaShake>();
        getCameraTransfromEventSO.onEventRaised += trans => _cameraTransform = trans;
    }

    private void OnDisable()
    {
        getCameraTransfromEventSO.onEventRaised -= trans => _cameraTransform = trans;
    }

    // 射出该球
    public void Shoot()
    {
        if (hit)
        {
            return;
        }

        StartCoroutine(HitPause());
        
        _kickAudio.PlayAudio();
        _shakeCinema.Shake();
        
        hit = true;
        _rigidbody.velocity = Vector3.zero;
        (_hitDirection.y, _hitDirection.z) = (_hitDirection.z, _hitDirection.y);
        _rigidbody.AddForce(_hitDirection.normalized * hitForce, ForceMode.Impulse);
    }

    // 不断更新摄像机四元数
    private void FixedUpdate()
    {
        _cameraQuaternion = _cameraTransform.rotation;
        ChangeShootDirection(_communication._quat);
    }

    // 打击时停
    private IEnumerator HitPause()
    {
        Time.timeScale = 0;
        // WaitForSecondsRealtime不受事件缩放影响
        yield return new WaitForSecondsRealtime(hitPauseTime / 60f);
        Time.timeScale = 1;
    }
    
    // 根据imu初始姿态和当前姿态以及摄像机朝向来更新踢球方向
    private void ChangeShootDirection(float[] quat)
    {
        var quaternion = new Quaternion(quat[0], quat[1], quat[2], quat[3]);
        var imuMatrix = quaternion.ConvertToMatrix();
        var initimuMatrix = SetIMUInitialQuaternion.imuInitQuaternionInv.ConvertToMatrix();
        var cameraMatrix = _cameraQuaternion.ConvertToMatrix();

        _hitDirection = SwapYAndZAxesInMatrix(imuMatrix * initimuMatrix) * cameraMatrix * Vector3.forward;
    }
    
    // 由于imu的坐标系和Unity默认不同，需要调换y和z，上面的踢球的力也需要调换
    private Matrix4x4 SwapYAndZAxesInMatrix(Matrix4x4 originalMatrix)
    {
        // 获取原始矩阵的列
        var yColumn = originalMatrix.GetColumn(1);
        var zColumn = originalMatrix.GetColumn(2);
        
        // 交换 y 和 z 列
        originalMatrix.SetColumn(1, zColumn);
        originalMatrix.SetColumn(2, yColumn);

        return originalMatrix;
    }
}
