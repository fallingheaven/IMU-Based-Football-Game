using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public long score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void UpdateScore(int a)
    {
        score += a;
    }
}
