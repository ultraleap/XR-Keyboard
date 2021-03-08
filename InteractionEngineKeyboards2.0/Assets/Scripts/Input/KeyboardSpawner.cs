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
    public Vector3 DistanceFromHead;
    public RelativeTo RotationRelativeTo;
    public RelativeTo PositionRelativeTo;
    private bool keyboardActive = false;
    private GameObject currentlySelected;

    private void Start()
    {
        DespawnKeyboard();
    }

    // Update is called once per frame
    void Update()
    {
        currentlySelected = EventSystem.current.currentSelectedGameObject;
        if (currentlySelected == null)
        {
            if (keyboardActive)
            {
                DespawnKeyboard();
            }
            return;
        }
        if (currentlySelected.GetComponent<TMP_InputField>() != null
        || currentlySelected.GetComponent<InputField>() != null)
        {
            if (!keyboardActive)
            {
                SpawnKeyboard();
            }
        }
        else
        {
            if (keyboardActive)
            {
                DespawnKeyboard();
            }
        }
    }
    private void SpawnKeyboard()
    {

        keyboardActive = true;
        GrabBall.parent.gameObject.SetActive(keyboardActive);

        if (PositionRelativeTo == RelativeTo.HEAD)
        {
            SetPosition(head.position + DistanceFromHead);
        }
        else if (PositionRelativeTo == RelativeTo.TEXT_FIELD)
        {
            if (currentlySelected != null)
            {
                SetPositionRelativeTo(currentlySelected.transform.position);
            }
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
    private void DespawnKeyboard()
    {
        keyboardActive = false;
        GrabBall.parent.gameObject.SetActive(keyboardActive);
    }

    private void SetPositionRelativeTo(Vector3 _relativePosition)
    {
        Vector3 directionVector = Vector3.Normalize(_relativePosition - head.position);
        Vector3 newPosition = head.position + (directionVector * DistanceFromHead.z);

        newPosition.y = head.position.y + DistanceFromHead.y;
        SetPosition(newPosition);
    }

    private void SetPosition(Vector3 _newPosition)
    {
        //As the grab ball's pivot point isn't in the centre of the keyboard, 
        // we want to work out how far we'd need to move the keyboard to be offset from the head
        // and then apply that offset to the grab ball
      //  Vector3 offset = _newPosition - KeyboardCentre.position;
        GrabBall.GetComponent<Rigidbody>().position = _newPosition;
    }
}