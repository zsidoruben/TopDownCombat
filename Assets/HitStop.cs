using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool waiting = false;
    public void Stop(float dur)
    {
        if (waiting)
        {
            return;
        }
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(dur));
    }

    IEnumerator Wait(float dur)
    { 

        waiting = true;
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
