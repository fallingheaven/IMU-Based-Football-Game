using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateYRotation : MonoBehaviour
{
    public void ChangeRotation(float[] quat)
    {
        var quaternion = new Quaternion(quat[0], quat[1], quat[2], quat[3]);
        transform.rotation = Quaternion.Euler(0, quaternion.eulerAngles.y, 0);
    }
}
