using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionExtensions
{
    // 用于四元数转化为矩阵
    public static Matrix4x4 ConvertToMatrix(this Quaternion quaternion)
    {
        Matrix4x4 matrix = new Matrix4x4();

        // Calculate the elements of the rotation matrix
        var xx = quaternion.x * quaternion.x;
        var xy = quaternion.x * quaternion.y;
        var xz = quaternion.x * quaternion.z;
        var xw = quaternion.x * quaternion.w;

        var yy = quaternion.y * quaternion.y;
        var yz = quaternion.y * quaternion.z;
        var yw = quaternion.y * quaternion.w;

        var zz = quaternion.z * quaternion.z;
        var zw = quaternion.z * quaternion.w;

        matrix[0, 0] = 1.0f - 2.0f * (yy + zz);
        matrix[0, 1] = 2.0f * (xy - zw);
        matrix[0, 2] = 2.0f * (xz + yw);
        matrix[0, 3] = 0.0f;

        matrix[1, 0] = 2.0f * (xy + zw);
        matrix[1, 1] = 1.0f - 2.0f * (xx + zz);
        matrix[1, 2] = 2.0f * (yz - xw);
        matrix[1, 3] = 0.0f;

        matrix[2, 0] = 2.0f * (xz - yw);
        matrix[2, 1] = 2.0f * (yz + xw);
        matrix[2, 2] = 1.0f - 2.0f * (xx + yy);
        matrix[2, 3] = 0.0f;

        matrix[3, 0] = 0.0f;
        matrix[3, 1] = 0.0f;
        matrix[3, 2] = 0.0f;
        matrix[3, 3] = 1.0f;

        return matrix;
    }
}
