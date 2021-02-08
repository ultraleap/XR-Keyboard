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
        Bounds bounds = mesh.CalculateActualBounds();
       

        sizeValue.text = 
        $"X: {bounds.size.x * 100:0.00}cm\n" +
        $"Y: {bounds.size.y * 100:0.00}cm\n" +
        $"Z: {bounds.size.z * 100:0.00}cm";
    }
}
