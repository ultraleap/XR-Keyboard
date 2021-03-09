using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class SizeOfMeshOutput : MonoBehaviour
{
    public TextMeshPro sizeValue;
    public MeshRenderer mesh;

    // Update is called once per frame
    void Update()
    {
        Vector3 size = mesh.bounds.size;
       

        sizeValue.text = 
        $"X: {size.x * 100:0.00}cm\n" +
        $"Y: {size.y * 100:0.00}cm\n" +
        $"Z: {size.z * 100:0.00}cm";
    }
}
