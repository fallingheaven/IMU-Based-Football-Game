using UnityEngine;

public class Wind : MonoBehaviour
{
    public WindSO windSO;
    private Rigidbody _rigidbody;
    private ShootFootball _shootFootball;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _shootFootball = GetComponent<ShootFootball>();
    }

    private void FixedUpdate()
    {
        if (!_shootFootball.hit) return;
        
        _rigidbody.AddForce(windSO.windForce * Time.deltaTime, ForceMode.VelocityChange);
    }
}
