using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardResizer : MonoBehaviour
{

    [BoxGroup("Setup")] public VerticalLayoutGroup KeyboardKeysParent;
    [BoxGroup("Setup")] public RectTransform prefabParent;
    [BoxGroup("Setup")] public List<HorizontalLayoutGroup> keyboardRows;

    [BoxGroup("Size")] public float gapSize;
    [BoxGroup("Size")] public float buttonSize;


    [Button]
    private void ResizeKeyboard()
    {
        SpaceKeyboard();
        SizeButtons();
        SizePanel();


    }

    private void SizePanel()
    {
        float longestRow = 0;
        foreach (HorizontalLayoutGroup row in keyboardRows)
        {
            RectTransform rowTransform = row.GetComponent<RectTransform>();
            Vector2 horizontalSizeDelta = new Vector2(0, buttonSize / rowTransform.lossyScale.y);
            float scaledGapSize = gapSize / rowTransform.lossyScale.y;
            float currentRowLength = 0;

            foreach (RectTransform button in rowTransform)
            {
                float scaledButtonSize = button.sizeDelta.x * button.lossyScale.x;
                horizontalSizeDelta.x += scaledButtonSize / rowTransform.lossyScale.x;
                horizontalSizeDelta.x += scaledGapSize;

                currentRowLength += scaledButtonSize;
                currentRowLength += gapSize;
            }
            horizontalSizeDelta.x -= scaledGapSize;
            horizontalSizeDelta.x += buttonSize / 2;
            rowTransform.sizeDelta = horizontalSizeDelta;
            MarkAsDirty(rowTransform, $"Update Size Delta of {rowTransform.name}");

            currentRowLength -= gapSize;
            longestRow = Mathf.Max(currentRowLength, longestRow);

        }

        RectTransform verticalGroup = KeyboardKeysParent.GetComponent<RectTransform>();
        Vector2 verticalSizeDelta = new Vector2()
        {
            x = longestRow / verticalGroup.lossyScale.x,
            y = ((buttonSize * keyboardRows.Count) + (gapSize * (keyboardRows.Count - 1))) / verticalGroup.lossyScale.y
        };

        verticalGroup.sizeDelta = verticalSizeDelta;
        prefabParent.sizeDelta = verticalSizeDelta;
        MarkAsDirty(verticalGroup, $"Update Size Delta of {verticalGroup.name}");
        MarkAsDirty(prefabParent, $"Update Size Delta of {prefabParent.name}");
    }

    private void SpaceKeyboard()
    {
        KeyboardKeysParent.spacing = gapSize / KeyboardKeysParent.transform.lossyScale.y;
        MarkAsDirty(KeyboardKeysParent, $"Update spacing of {KeyboardKeysParent.name}");

        foreach (HorizontalLayoutGroup horizontalLayoutGroup in keyboardRows)
        {
            horizontalLayoutGroup.spacing = gapSize / horizontalLayoutGroup.transform.lossyScale.x;
            MarkAsDirty(horizontalLayoutGroup, $"Update spacing of {horizontalLayoutGroup.name}");
        }
    }

    private void SizeButtons()
    {
        foreach (HorizontalLayoutGroup row in keyboardRows)
        {
            foreach (RectTransform buttonTransform in row.transform)
            {
                Vector2 scaledGapSize = new Vector2(gapSize / buttonTransform.transform.lossyScale.x, gapSize / buttonTransform.transform.lossyScale.y);
                Vector2 scaledButtonSize = new Vector2(buttonSize / buttonTransform.transform.lossyScale.x, buttonSize / buttonTransform.transform.lossyScale.y);

                Vector2 sizeDelta = scaledButtonSize;
                TextInputButton uiTextInputButton = buttonTransform.GetComponentInChildren<TextInputButton>();
                if (buttonTransform.gameObject.name == "Padding")
                {
                    sizeDelta *= 0.5f;
                }
                else
                {
                    switch (uiTextInputButton.NeutralKey)
                    {
                        case KeyCode.Space:
                            sizeDelta.x = (scaledButtonSize.x * 9.5f) + (scaledGapSize.x * 8);

                            break;
                        case KeyCode.Backspace:
                        case KeyCode.RightShift:
                            sizeDelta.x *= 1.5f;

                            break;
                        case KeyCode.Return:
                            sizeDelta.x = scaledButtonSize.x * 2f;
                            break;
                    }
                }
                buttonTransform.sizeDelta = sizeDelta;
                MarkAsDirty(buttonTransform, $"Update sizeDelta of {buttonTransform.name}");
            }
        }
    }

    private void MarkAsDirty(UnityEngine.Object o, string message)
    {
        Undo.RecordObject(o, message);
        PrefabUtility.RecordPrefabInstancePropertyModifications(o);
    }
}
