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
        distanceValues.text = $"X: {distance.x}\nY: {distance.y}\nZ: {distance.z}";
        rotationValues.text = $"X: {rotationDelta.x}\nY: {rotationDelta.y}\nZ: {rotationDelta.z}";
    }
}
