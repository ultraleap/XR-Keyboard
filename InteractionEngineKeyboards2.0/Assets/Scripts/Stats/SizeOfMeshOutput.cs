using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SizeOfMeshOutput : MonoBehaviour
{
    public enum Axis
    {
        X, Y, Z
    }

    public TextMeshPro sizeValue;
    public MeshRenderer mesh;
    public Axis axis;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float size = 0;
        switch (axis)
        {
            case Axis.X:
                size = mesh.bounds.size.x * mesh.gameObject.transform.localScale.x;
                break;
            case Axis.Y:
                size = mesh.bounds.size.y * mesh.gameObject.transform.localScale.y;
                break;
            case Axis.Z:
                size = mesh.bounds.size.z * mesh.gameObject.transform.localScale.z;
                break;
        }
        sizeValue.text = $"{(size * 100).ToString("0.00")}cm";

    }
}
