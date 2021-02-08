using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceAndRotationOutput : MonoBehaviour
{
    public TextMeshPro distanceValues;
    public TextMeshPro rotationValues;

    public Transform origin;
    public Transform distanceRelativeTo;
    
    public Transform RotationRelativeTo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = distanceRelativeTo.position - origin.position;
        Vector3 rotationDelta = Quaternion.FromToRotation(origin.transform.forward, RotationRelativeTo.transform.forward).eulerAngles;
        distanceValues.text = $"X: {distance.x * 100:0.00}cm\nY: {distance.y * 100:0.00}cm\nZ: {distance.z * 100:0.00}cm";
        rotationValues.text = $"X: {rotationDelta.x:000.00}\nY: {rotationDelta.y:000.00}\nZ: {rotationDelta.z:000.00}";
    }
}
