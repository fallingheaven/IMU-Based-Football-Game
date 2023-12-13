using System;
using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem explode;
    public GameObject mainPart;
    
    public GameObjectEventSO returnBombEventSO;
    
    private Rigidbody _rigidbody;
    private SphereCollider _collider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<SphereCollider>();
    }

    public void PlayExplodeAnimation()
    {
        _rigidbody.velocity = Vector3.zero;
        _collider.enabled = false;
        
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        explode.Play();
        mainPart.SetActive(false);
        yield return new WaitForSeconds(1f);
        returnBombEventSO.RaiseEvent(gameObject);
    }
}
