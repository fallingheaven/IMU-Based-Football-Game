using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetIMUInitialQuaternion
{
    public static Quaternion imuInitQuaternionInv;

    public static void InitImuQuaternion(Quaternion quaternion)
    {
        imuInitQuaternionInv = Quaternion.Inverse(quaternion);
    }
}
