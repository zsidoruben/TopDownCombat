using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnSpawn : MonoBehaviour
{
    Explodable _explodable;
    // Start is called before the first frame update
    void Start()
    {
        _explodable = GetComponent<Explodable>();
        _explodable.explode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
