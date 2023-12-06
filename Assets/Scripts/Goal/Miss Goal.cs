using UnityEngine;

// miss判定球门（也可以不是球门样式）
public class MissGoal : MonoBehaviour, IFootballChecker
{
    public GameObjectEventSO returnFootballEventSO;
    public LayerMask footballLayer;
    
    private void Start()
    {
        footballLayer = LayerMask.NameToLayer("Ball");
    }
    
    public void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.GetComponent<Ball>().type)
        {
            case BallType.Football:
            {
                returnFootballEventSO.RaiseEvent(col.gameObject);
                break;
            }
            case BallType.Bomb:
            {
                Destroy(col.gameObject);
                break;
            }
            case BallType.Goldball:
            {
                break;
            }
            default:
            {
                break;
            }
        }
    }
}
