using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public GameObjectEventSO ReturnFootballEventSO;
    public FloatEventSO updateScoreEventSO;
    public LayerMask goalLayer, missLayer, kickLayer;
    private ShootFootball _shoot;
    public int addedScore = 1;

    private void Start()
    {
        _shoot = GetComponent<ShootFootball>();
        goalLayer = LayerMask.NameToLayer("Goal");
        missLayer = LayerMask.NameToLayer("Miss");
    }

    private void OnTriggerEnter(Collider col)
    {
        // 进到球门里就得分
        if (col.gameObject.layer == goalLayer)
        {
            updateScoreEventSO.RaiseEvent(addedScore);
            ReturnFootballEventSO.RaiseEvent(gameObject);
        }
        else if (col.gameObject.layer == missLayer)
        {
            ReturnFootballEventSO.RaiseEvent(gameObject);
        }
    }
}
