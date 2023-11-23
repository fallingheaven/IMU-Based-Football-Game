using UnityEngine;

// 用于resetIMU初始姿态的
public static class SetIMUInitialQuaternion
{
    public static Quaternion imuInitQuaternionInv;

    public static void InitImuQuaternion(Quaternion quaternion)
    {
        imuInitQuaternionInv = Quaternion.Inverse(quaternion);
    }
}
