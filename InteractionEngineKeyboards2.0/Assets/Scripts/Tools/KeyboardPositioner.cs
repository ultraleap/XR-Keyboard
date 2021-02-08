using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class KeyboardPositioner : MonoBehaviour
{
    public List<Transform> rowTransforms;
    public float buttonGap = 0.01f;

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




            // Vector3 newLocalPos = rowTransforms[i - 1].localPosition;

            // if (i % 2 == 1)
            // {
            //     //offset odd numbered rows
            //     newLocalPos.x += previousFirstButtonMeshExtents.x + buttonGap / 2;
            // }
            // //space button row out vertically
            // // newLocalPos.y -= ( ;
            // //ensure the z position lines up
            // newLocalPos.z = rowTransforms[0].localPosition.z;
            // rowTransforms[i].localPosition = newLocalPos;

            Vector3 translation = (previousFirstButtonMeshExtents.x + buttonGap + firstButtonMeshExtents.x) * -rowTransforms[i].transform.up.normalized;
            Debug.Log($"COLUMN: previousFirstButtonMeshExtents.x: {previousFirstButtonMeshExtents.x}, buttonGap: {buttonGap}, firstButtonMeshExtents.x: {firstButtonMeshExtents.x}");

            rowTransforms[i].transform.position += translation;
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

            Vector3 translation = (previousButtonMeshExtents.x + buttonGap + buttonMeshExtents.x) * button.right.normalized;
            Debug.Log($"ROW: previousButtonMeshExtents.x: {previousButtonMeshExtents.x}, buttonGap: {buttonGap}, buttonMeshExtents.x: {buttonMeshExtents.x}");

            button.position += translation;
        }

    }

    private Vector3 GetButtonCubeExtents(Transform row, int buttonIndex)
    {
        return row.GetChild(buttonIndex).Find(BUTTON_CUBE_NAME).lossyScale * 0.5f;
        Transform child = row.GetChild(buttonIndex).Find(BUTTON_CUBE_NAME);
        MeshRenderer m = child.GetComponent<MeshRenderer>();
        return child.GetComponent<MeshRenderer>().bounds.extents;
    }
}
