using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool isCounting = false;
    public float time = 10;

    // Update is called once per frame
    void Update()
    {
        if(isCounting)
        {
            time -= Time.deltaTime;
        }
    }

    public void StartCount()
    {
        isCounting = true;
    }

    public bool IsCounting()
    {
        return isCounting;
    }

    public float CurrentTime()
    {
        return time;
    }
}
