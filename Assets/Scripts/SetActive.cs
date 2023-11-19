using System;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject _gameObject;

    public void SetTargetActive()
    {
        _gameObject.SetActive(true);   
    }
}
