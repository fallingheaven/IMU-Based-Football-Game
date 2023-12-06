using System;
using UnityEngine;

public class SensitivityAnimationResponse : MonoBehaviour
{
    public VoidEventSO imuKickEventSO;

    private void OnEnable()
    {
        imuKickEventSO.onEventRaised += PlayAnimation;
    }

    private void OnDisable()
    {
        imuKickEventSO.onEventRaised -= PlayAnimation;
    }

    private void PlayAnimation()
    {
        Debug.Log("播放动画");
    }
}
