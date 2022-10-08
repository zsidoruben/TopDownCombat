using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float multiplier = 0.1f;
    IEnumerator Shake(float dur, float magnitude)
    {
        Vector3 OriginalPos = transform.position;

        float elapsed = 0;

        while (elapsed < dur)
        {
            
            float x = Random.Range(-1, 1) * magnitude * multiplier;
            float y = Random.Range(-1, 1) * magnitude * multiplier;
            transform.localPosition = new Vector3(x, y, OriginalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(0,0,0);
    }

    public void StartShake(float _dur, float _magnitude)
    {
        StartCoroutine(Shake(_dur, _magnitude));
    }
}
