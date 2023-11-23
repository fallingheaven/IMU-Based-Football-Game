using UnityEngine;

public class TransferCameraTransform : MonoBehaviour
{
    public TransformEventSO TransferCameraTransformEventSO;
    private void FixedUpdate()
    {
        TransferCameraTransformEventSO.RaiseEvent(transform);
    }
}
