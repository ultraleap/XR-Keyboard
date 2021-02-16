using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public Transform keyboard;
    public Vector3 DistanceFromHead;
    public Vector3 Angles;

    private Vector3 targetLocation;
    private Quaternion targetRotation;

    private void Start()
    {
        targetLocation = keyboard.position;
        targetRotation = keyboard.rotation;
    }
    private void Update()
    {
        if (keyboard.position != targetLocation || keyboard.rotation != targetRotation)
        {
            keyboard.position = Vector3.Lerp(keyboard.position, targetLocation, Time.deltaTime * 30);
            keyboard.rotation = Quaternion.Lerp(keyboard.rotation, targetRotation, Time.deltaTime * 30);
        }
    }
    public void SetPosition()
    {
        targetRotation = Quaternion.identity * Quaternion.Euler(Angles);
        targetLocation = head.position + DistanceFromHead;
    }
}
