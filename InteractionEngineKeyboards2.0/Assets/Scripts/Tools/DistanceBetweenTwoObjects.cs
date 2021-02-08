using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceBetweenTwoObjects : MonoBehaviour
{
    public Transform a, b;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer m = a.Find("Button Cube")?.GetComponent<MeshRenderer>();
        dist = Vector3.Distance(a.position, b.position);
        if (m != null)
        {
            dist -= m.bounds.size.x;
        }
    }
}
