﻿using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class InteractionButtonKeyboardResizer : MonoBehaviour
{
    public List<Transform> rowTransforms;
    public Transform KeyboardParent;
    public Transform panel;
    [BoxGroup("Button Gap")] public float buttonGapRow = 0.01f;
    [BoxGroup("Button Gap")] public float buttonGapColumn = 0.01f;
    public float standardButtonSize = 1.5f;

    private const string BUTTON_CUBE_NAME = "Button Cube";


    [Button("Position Keyboard")]
    private void PositionKeyboard()
    {
        ResizeButtons();
        ResizePanel();
        PositionButtons();
        PositionPanel();
        MovePivotPoint();
        PositionTools();
    }

    private void PositionPanel()
    {
        Vector3 topLeft = rowTransforms[0].GetChild(0).position;
        Vector3 bottomRight = rowTransforms[rowTransforms.Count - 1].GetChild(rowTransforms[rowTransforms.Count - 1].childCount - 1).position;
        Vector3 newPanelPos = new Vector3()
        {
            x = topLeft.x + ((bottomRight.x - topLeft.x) / 2),
            y = bottomRight.y + ((topLeft.y - bottomRight.y) / 2) - standardButtonSize - (buttonGapColumn / 2),
            z = topLeft.z,
        };
        panel.position = newPanelPos;
        newPanelPos = panel.localPosition;
        newPanelPos.z = 0.01f;
        panel.localPosition = newPanelPos;
    }

    private void ResizeButtons()
    {
        foreach (Transform row in rowTransforms)
        {
            foreach (Transform button in row)
            {
                TextInputButton textInputButton = button.GetComponent<TextInputButton>();
                Bounds bounds = button.Find(BUTTON_CUBE_NAME).GetComponent<MeshRenderer>().bounds;
                Vector3 newScale = new Vector3()
                {
                    x = standardButtonSize / (bounds.size.x / button.localScale.x),
                    y = standardButtonSize / (bounds.size.x / button.localScale.x),
                    z = button.localScale.z
                };

                switch (textInputButton.NeutralKey)
                {
                    case KeyCode.Space:
                        newScale.x = ((standardButtonSize * 9.5f) + (buttonGapRow * 8)) / (bounds.size.x / button.localScale.x);
                        button.localScale = newScale;
                        break;
                    case KeyCode.Backspace:
                    case KeyCode.RightShift:
                        newScale.x *= 1.5f;
                        button.localScale = newScale;
                        break;
                    case KeyCode.Return:
                        newScale.x = ((standardButtonSize * 2f) + buttonGapRow / 2) / (bounds.size.x / button.localScale.x);
                        button.localScale = newScale;
                        break;
                    default:
                        button.localScale = newScale;
                        break;
                }
            }
        }
    }

    private void ResizePanel()
    {
        Bounds bounds = panel.GetComponent<MeshRenderer>().bounds;
        Vector3 newScale = new Vector3()
        {
            x = ((standardButtonSize * 11.5f) + (buttonGapRow * 12)) / (bounds.size.x / panel.localScale.x),
            y = ((standardButtonSize * 4f) + (buttonGapRow * 5)) / (bounds.size.x / panel.localScale.x),
            z = panel.localScale.z
        };
        panel.localScale = newScale;
    }

    private void PositionButtons()
    {
        // Position the first row relative to the position of the first button
        PositionRow(rowTransforms[0]);

        for (int i = 1; i < rowTransforms.Count; i++)
        {
            //reset row position
            rowTransforms[i].position = rowTransforms[i - 1].position;

            //Space the buttons out in the row
            PositionRow(rowTransforms[i]);

            Vector3 firstButtonMeshExtents = GetButtonCubeExtents(rowTransforms[i], 0);
            Vector3 previousFirstButtonMeshExtents = GetButtonCubeExtents(rowTransforms[i - 1], 0);

            Vector3 translation = (previousFirstButtonMeshExtents.x + buttonGapColumn + firstButtonMeshExtents.x) * -rowTransforms[i].transform.up.normalized;
            rowTransforms[i].transform.position += translation;

            Vector3 newLocalPos = rowTransforms[i].localPosition;
            //offset second row
            if (i == 1)
            {
                newLocalPos.x += previousFirstButtonMeshExtents.x + buttonGapRow / 2;
            }
            newLocalPos.z = 0;
            rowTransforms[i].localPosition = newLocalPos;
        }
    }

    private void PositionRow(Transform rowParentTransform)
    {
        Vector3 initialRowPos = rowParentTransform.transform.GetChild(0).position;
        initialRowPos.x = rowTransforms[0].GetChild(0).position.x;
        //position the first button in the row in the same x position as the first button in the first row
        rowParentTransform.GetChild(0).position = initialRowPos;

        for (int i = 1; i < rowParentTransform.childCount; i++)
        {
            Transform button = rowParentTransform.GetChild(i);
            Vector3 buttonMeshExtents = GetButtonCubeExtents(rowParentTransform, i);

            Transform previousButton = rowParentTransform.GetChild(i - 1);
            Vector3 previousButtonMeshExtents = GetButtonCubeExtents(rowParentTransform, i - 1);

            button.position = previousButton.position;
            Vector3 translation = (previousButtonMeshExtents.x + buttonGapRow + buttonMeshExtents.x) * button.right.normalized;
            button.position += translation;
        }
    }

    private void MovePivotPoint()
    {
        Transform keyboardParent = KeyboardParent.Find("Keyboard");
        Vector3 delta = keyboardParent.position - panel.position;
        delta.z = 0;
        foreach (Transform child in keyboardParent)
        {
            child.position += delta;
        }

        delta = KeyboardParent.position - panel.position;
        delta.z = 0;
        foreach (Transform child in transform)
        {
            child.position += delta;
        }
    }

    private Vector3 GetButtonCubeExtents(Transform row, int buttonIndex)
    {
        return row.GetChild(buttonIndex).Find(BUTTON_CUBE_NAME).lossyScale * 0.5f;
    }

    private void PositionTools()
    {
        Transform keyboard = KeyboardParent.Find("Keyboard");
        Transform textReceiver = KeyboardParent.Find("Text Receiver");
        Vector3 textReceiverPos = keyboard.localPosition;
        textReceiverPos.y += (standardButtonSize * 6) + (buttonGapColumn * 2);
        textReceiver.localPosition = textReceiverPos;

        Transform grabSphere = KeyboardParent.Find("Grab Sphere");
        Vector3 grabSpherePos = keyboard.localPosition;
        grabSpherePos.y -= (standardButtonSize * 3) + (buttonGapColumn * 2);
        grabSphere.localPosition = grabSpherePos;
    }
}