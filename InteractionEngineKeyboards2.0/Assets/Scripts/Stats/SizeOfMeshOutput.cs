using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class SizeOfMeshOutput : MonoBehaviour
{
    public enum Axis
    {
        X, Y, Z
    }

    public TextMeshPro sizeValue;
    public MeshRenderer mesh;
    public Axis axis;
    private Quaternion rot;

    // Update is called once per frame
    void Update()
    {
        rot = mesh.transform.rotation;

        mesh.transform.rotation = Quaternion.identity;
        float size = 0;
        switch (axis)
        {
            case Axis.X:
                size = mesh.bounds.size.x;
                break;
            case Axis.Y:
                size = mesh.bounds.size.y;
                break;
            case Axis.Z:
                size = mesh.bounds.size.z;
                break;
        }
        sizeValue.text = $"{axis}: {size * 100:0.00}cm";
        mesh.transform.rotation = rot;
        Physics.SyncTransforms();
    }
}
