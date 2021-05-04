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

    private Coroutine moveToRoutine;

    private void Start()
    {
        targetLocation = position.position;
        targetRotation = position.rotation;
    }

    private void Update()
    {
    }

    public void SetPosition()
    {

        Vector3 newPosition = head.position + (head.forward * DistanceFromHead.z);
        newPosition.y = head.position.y + DistanceFromHead.y;

        targetLocation = newPosition;

        if (moveToRoutine != null)
        {
            StopCoroutine(moveToRoutine);
        }
        moveToRoutine = StartCoroutine("MoveToTarget");
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(position.position, targetLocation) > 0.005f)
        {
            position.position = Vector3.Lerp(position.position, targetLocation, Time.deltaTime * 30);
            position.rotation = Quaternion.Lerp(position.rotation, targetRotation, Time.deltaTime * 30);

            Vector3 pos = position.position;
            pos.y = head.position.y;
            Vector3 forward = pos - head.position;
            targetRotation = Quaternion.LookRotation(forward, Vector3.up);

            yield return new WaitForEndOfFrame();
        }
    }
}
