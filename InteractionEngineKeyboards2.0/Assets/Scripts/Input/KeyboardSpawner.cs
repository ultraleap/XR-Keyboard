using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardSpawner : MonoBehaviour
{
    public enum RelativeTo
    {
        HEAD,
        TEXT_FIELD
    }
    public Transform head;
    public Transform GrabBall;
    public GrabGimbal GrabGimbal;
    public Transform KeyboardCentre;
    public Vector3 DistanceFromHead = new Vector3(0, -0.385f, 0.4f);
    public RelativeTo PositionRelativeTo = RelativeTo.TEXT_FIELD;
    public RelativeTo RotationRelativeTo = RelativeTo.HEAD;
    private bool keyboardActive = false;
    private Vector3 offset;

    private void Start()
    {
        offset = GrabBall.position - KeyboardCentre.position;
        DespawnKeyboard();
    }

    public void SpawnKeyboard(Transform currentlySelected)
    {
        if (keyboardActive)
        {
            return;
        }
        else
        {
            keyboardActive = true;
        }
        GrabBall.parent.gameObject.SetActive(keyboardActive);

        if (PositionRelativeTo == RelativeTo.HEAD)
        {
            SetPosition(head.position + DistanceFromHead);
        }
        else if (PositionRelativeTo == RelativeTo.TEXT_FIELD)
        {
            SetPositionRelativeTo(currentlySelected.transform.position);
        }

        if (RotationRelativeTo == RelativeTo.HEAD)
        {
            GrabGimbal.UpdateTargetRotation();
        }
        else if (RotationRelativeTo == RelativeTo.TEXT_FIELD)
        {
            GrabGimbal.targetRotation = currentlySelected.transform.rotation;
        }
    }
    public void DespawnKeyboard()
    {
        print("DESPAWN");
        keyboardActive = false;
        if (GrabBall != null)
        {
            GrabBall.parent.gameObject.SetActive(keyboardActive);
        }
    }

    private void SetPositionRelativeTo(Vector3 _relativePosition)
    {
        Vector3 directionVector = Vector3.Normalize(_relativePosition - head.position);
        Vector3 newPosition = head.position + (directionVector * (DistanceFromHead.z + offset.z));
        newPosition.y = head.position.y + DistanceFromHead.y + offset.y;
        SetPosition(newPosition);
    }

    private void SetPosition(Vector3 _newPosition)
    {
        GrabBall.GetComponent<Rigidbody>().position = _newPosition;
    }
}