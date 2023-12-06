using System;
using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem explode;
    public GameObject mainPart;
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
        Destroy(_collider);
        
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        explode.Play();
        Destroy(mainPart);
        // Debug.Log(explode.time);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
