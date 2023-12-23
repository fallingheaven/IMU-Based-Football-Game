using UnityEngine;

public class BlockFootball : MonoBehaviour
{
    public LayerMask footballLayer;
    public GameObjectFloatFloatEventSO delayedReturnFootballEventSO;
    
    private void OnEnable()
    {
        footballLayer = LayerMask.NameToLayer("Ball");
    }

    private void OnTriggerEnter(Collider col)
    {
        // Debug.Log("trigger");
        if (col.gameObject.layer == footballLayer)
        {
            Debug.Log("Block!");
            col.GetComponent<Rigidbody>().velocity = Vector3.zero;
            col.GetComponent<Rigidbody>().AddForce((col.transform.position - transform.position).normalized * 10f, 
                ForceMode.Impulse);
            delayedReturnFootballEventSO.RaiseEvent(col.gameObject, 3f);
        }
    }
}
