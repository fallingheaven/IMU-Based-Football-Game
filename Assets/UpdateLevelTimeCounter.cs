using UnityEngine;

public class UpdateLevelTimeCounter : MonoBehaviour
{
    public FloatFloatEventSO updateLevelTimeCounterEventSO;

    private void OnEnable()
    {
        updateLevelTimeCounterEventSO.onEventRaised += ChangeBarRotation;
    }

    private void OnDisable()
    {
        updateLevelTimeCounterEventSO.onEventRaised -= ChangeBarRotation;
    }

    private void ChangeBarRotation(float currentTime, float totalTime)
    {
        var target = new Vector3(0, 0, 360 * currentTime / totalTime);
        transform.rotation = Quaternion.Euler(target);
    }
}
