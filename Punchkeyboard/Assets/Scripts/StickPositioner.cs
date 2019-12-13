using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPositioner : MonoBehaviour
{
    [SerializeField]
    Transform posMain, posStabiliser, rotMain, rotStabiliser;

    [SerializeField]
    Vector3 rotationOffset, positionOffset;

    // Update is called once per frame
    void Update()
    {
        transform.position = rotMain.position + positionOffset;
        transform.rotation =  Quaternion.Euler(posMain.rotation.eulerAngles + rotationOffset);
    }
}
