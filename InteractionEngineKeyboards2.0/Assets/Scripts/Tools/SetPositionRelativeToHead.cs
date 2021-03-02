﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRelativeToHead : MonoBehaviour
{
    public Transform head;
    public Transform position;
    public Vector3 DistanceFromHead;
    public Vector3 Angles;

    private Vector3 targetLocation;
    private Quaternion targetRotation;

    private void Start()
    {
        targetLocation = position.position;
        targetRotation = position.rotation;
    }
    private void Update()
    {
        if (position.position != targetLocation || position.rotation != targetRotation)
        {
            position.position = Vector3.Lerp(position.position, targetLocation, Time.deltaTime * 30);
            position.rotation = Quaternion.Lerp(position.rotation, targetRotation, Time.deltaTime * 30);
        }
    }
    public void SetPosition()
    {
        targetRotation = Quaternion.identity * Quaternion.Euler(Angles);
        targetLocation = head.position + DistanceFromHead;
    }
}
