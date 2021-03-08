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
    private GameObject currentlySelected;
    private Vector3 offset;

    private void Start()
    {
        offset = GrabBall.position - KeyboardCentre.position;
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
        GrabBall.GetComponent<Rigidbody>().position = _newPosition + offset;
    }
}