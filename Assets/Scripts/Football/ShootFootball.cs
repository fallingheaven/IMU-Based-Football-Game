using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShootFootball : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private UnityPythonCommunication _communication;
    
    public bool hit = false;
    // public bool reachable;
    private Vector3 _hitDirection;
    public float hitForce = 15f;

    private Transform _cameraTransform;
    private Quaternion _cameraQuaternion;
    private void Start()
    {
        // hit = false;
        // reachable = false;
    }

    private void OnEnable()
    {
        _communication = GameObject.Find("Communication").GetComponent<UnityPythonCommunication>();
        _rigidbody = GetComponent<Rigidbody>();
        _cameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    }

    public void Shoot()
    {
        // Debug.Log("准备射门");
        if (hit)
        {
            return;
        }
        
        // Debug.Log($"成功射门, {(transform.position - position).normalized * hitForce}");
        Debug.Log($"发射 {_hitDirection}");
        hit = true;
        _rigidbody.velocity = Vector3.zero;
        (_hitDirection.y, _hitDirection.z) = (_hitDirection.z, _hitDirection.y);
        // _hitDirection.x *= -2;
        // _hitDirection.y *= -2;
        _rigidbody.AddForce(_hitDirection.normalized * hitForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _cameraQuaternion = _cameraTransform.rotation;
        ChangeShootDirection(_communication._quat);
        Debug.Log($"{_hitDirection}");
    }

    public void ChangeShootDirection(float[] quat)
    {
        var quaternion = new Quaternion(quat[0], quat[1], quat[2], quat[3]);
        var imuMatrix = quaternion.ConvertToMatrix();
        var initimuMatrix = SetIMUInitialQuaternion.imuInitQuaternionInv.ConvertToMatrix();
        var cameraMatrix = _cameraQuaternion.ConvertToMatrix();

        _hitDirection = SwapYAndZAxesInMatrix(imuMatrix * initimuMatrix) * cameraMatrix * Vector3.forward;
        // Debug.Log($"更新发射方向 {_hitDirection}");
        // Debug.Log(SetIMUInitialQuaternion.imuInitQuaternionInv);
    }
    
    private Matrix4x4 SwapYAndZAxesInMatrix(Matrix4x4 originalMatrix)
    {
        // 获取原始矩阵的列
        // Vector4 xColumn = originalMatrix.GetColumn(0);
        Vector4 yColumn = originalMatrix.GetColumn(1);
        Vector4 zColumn = originalMatrix.GetColumn(2);
        
        // 交换 y 和 z 列
        originalMatrix.SetColumn(1, zColumn);
        originalMatrix.SetColumn(2, yColumn);

        return originalMatrix;
    }
}
