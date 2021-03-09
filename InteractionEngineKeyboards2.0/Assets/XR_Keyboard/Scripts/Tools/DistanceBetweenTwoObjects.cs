using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceBetweenTwoObjects : MonoBehaviour
{
    public Transform a, b;
    public float dist;
    public Vector3 vDist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(a.position, b.position);
        vDist = a.position - b.position;
    }   
}
