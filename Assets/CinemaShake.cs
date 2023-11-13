using UnityEngine;

public class CinemaShake : MonoBehaviour
{
    public VoidEventSO voidEventSO;

    public void Shake()
    {
        voidEventSO.RaiseEvent();
    }
}
