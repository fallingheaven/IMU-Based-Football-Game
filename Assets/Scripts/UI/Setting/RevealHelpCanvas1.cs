using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class RevealHelpCanvas1 : MonoBehaviour
{
    private int _index;
    private int _maxindex;
    public Canvas[] canvas;

    private void OnEnable()
    {
        _maxindex=1;
        _index = 0;
    }

    public void Next()
    {
        if (_index < _maxindex)
        {
            canvas[_index].gameObject.SetActive((false));
            _index++;
            canvas[_index].gameObject.SetActive(true);
        }
    }
    public void Last()
    {
        if(_index>0)
        {
            canvas[_index].gameObject.SetActive((false));
            _index--;
            canvas[_index].gameObject.SetActive(true);
        }
    }
}