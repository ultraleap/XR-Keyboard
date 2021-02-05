using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        distanceValues.text = $"X: {distance.x.ToString("0.00")}\nY: {distance.y.ToString("0.00")}\nZ: {distance.z.ToString("0.00")}";
        rotationValues.text = $"X: {rotationDelta.x.ToString("0.00")}\nY: {rotationDelta.y.ToString("0.00")}\nZ: {rotationDelta.z.ToString("0.00")}";
    }
}
