using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateRotation : MonoBehaviour
{
    public void ChangeRotation(float[] quat)
    {
        transform.rotation = new Quaternion(quat[0], quat[1], quat[2], quat[3]);
    }
}
