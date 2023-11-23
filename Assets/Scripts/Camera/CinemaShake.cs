using UnityEngine;

public class CinemaShake : MonoBehaviour
{
    public VoidEventSO CinemaShakeEventSO;

    public void Shake()
    {
        CinemaShakeEventSO.RaiseEvent();
    }
}
