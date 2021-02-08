using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KeyboardPositioner : MonoBehaviour
{
    public List<Transform> rowTransforms;
    public float buttonGapRow = 0.01f;
    public float buttonGapColumn = 0.01f;

    private const string BUTTON_CUBE_NAME = "Button Cube";

    [Button("Position Rows")]
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

            //offset odd numbered rows
            if (i % 2 == 1)
            {
                Vector3 newLocalPos = rowTransforms[i].localPosition;
                newLocalPos.x += previousFirstButtonMeshExtents.x + buttonGapRow / 2;
                rowTransforms[i].localPosition = newLocalPos;
            }
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

    private Vector3 GetButtonCubeExtents(Transform row, int buttonIndex)
    {
        return row.GetChild(buttonIndex).Find(BUTTON_CUBE_NAME).lossyScale * 0.5f;
    }
}
