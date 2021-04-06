using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardResizer : MonoBehaviour
{

    [BoxGroup("Setup")] public VerticalLayoutGroup KeyboardKeysParent;
    [BoxGroup("Setup")] public VerticalLayoutGroup KeyboardShadowsParent;
    [BoxGroup("Setup")] public RectTransform prefabParent;
    private List<HorizontalLayoutGroup> keyboardKeysRows;
    private List<HorizontalLayoutGroup> keyboardShadowsRows;

    [BoxGroup("Size")] public float gapSize = 0.005f;
    [BoxGroup("Size")] public float keySize = 0.4f;
    [BoxGroup("Size")] public Vector2 panelPaddingRelativeToKeySize = Vector2.zero * 0.5f;
    [BoxGroup("Size")] public float colliderDepth = 0.01f;

    [BoxGroup("Button Sizing")] public float SpaceSizeRelativeToKeySize = 9.375f; 
    [BoxGroup("Button Sizing")] public float SpaceSizeRelativeToGapSize = 9f; 
    [BoxGroup("Button Sizing")] public float BackspaceSizeRelativeToKeySize = 1.5f;
    [BoxGroup("Button Sizing")] public float RightShiftSizeRelativeToKeySize = 1.5f;
    [BoxGroup("Button Sizing")] public float ReturnSizeRelativeToKeySize = 2f;
    private const float PADDING_SIZE_MULTIPLIER = 0.5f;


    [Button]
    public void ResizeKeyboard()
    {
        keyboardKeysRows = KeyboardKeysParent.GetComponentsInChildren<HorizontalLayoutGroup>().ToList();
        keyboardShadowsRows = KeyboardShadowsParent.GetComponentsInChildren<HorizontalLayoutGroup>().ToList();
        ValidateRows();
        SpaceKeyboard();
        SizeKeys();
        SizePanel();
        ResizeColliders();
    }

    private void ValidateRows()
    {
        if (keyboardKeysRows.Count != keyboardShadowsRows.Count)
        {
            throw new System.Exception(
                "Keyboard Key Row Count & Keyboard Shadow Row Count are mismatched.\n"
                 + "Check that both have the same number of horizontal layout groups"
            );
        }

        for (int i = 0; i < keyboardKeysRows.Count; i++)
        {
            if (keyboardKeysRows[i].transform.childCount != keyboardShadowsRows[i].transform.childCount)
            {
                throw new System.Exception(
                    $"Keyboard Key Row {i} & Keyboard Shadow Row {i} have a different number of children.\n"
                     + "Ensure that every key has a matching shadow object."
                );
            }
        }
    }

    //Loop through each horizontal/vertical layout group & set the spacing to be correct 
    private void SpaceKeyboard()
    {
        UpdateVerticalLayoutGroupSpacing(KeyboardKeysParent);
        UpdateVerticalLayoutGroupSpacing(KeyboardShadowsParent);

        for (int i = 0; i < keyboardKeysRows.Count; i++)
        {
            UpdateHorizontalLayoutGroupSpacing(keyboardKeysRows[i]);
            UpdateHorizontalLayoutGroupSpacing(keyboardShadowsRows[i]);
        }
    }

    private void UpdateVerticalLayoutGroupSpacing(VerticalLayoutGroup verticalLayoutGroup)
    {
        verticalLayoutGroup.spacing = gapSize / verticalLayoutGroup.transform.lossyScale.y;
        MarkAsDirty(verticalLayoutGroup, $"Update spacing of {verticalLayoutGroup.name}");
    }
    private void UpdateHorizontalLayoutGroupSpacing(HorizontalLayoutGroup horizontalLayoutGroup)
    {
        horizontalLayoutGroup.spacing = gapSize / horizontalLayoutGroup.transform.lossyScale.x;
        MarkAsDirty(horizontalLayoutGroup, $"Update spacing of {horizontalLayoutGroup.name}");
    }

    // Loop through each key setting their sizeDelta
    private void SizeKeys()
    {
        for (int i = 0; i < keyboardKeysRows.Count; i++)
        {
            HorizontalLayoutGroup row = keyboardKeysRows[i];
            for (int j = 0; j < row.transform.childCount; j++)
            {
                RectTransform keyTransform = row.transform.GetChild(j).GetComponent<RectTransform>();
                Vector2 scaledGapSize = new Vector2(gapSize / keyTransform.transform.lossyScale.x, gapSize / keyTransform.transform.lossyScale.y);
                Vector2 scaledKeySize = new Vector2(keySize / keyTransform.transform.lossyScale.x, keySize / keyTransform.transform.lossyScale.y);

                Vector2 sizeDelta = scaledKeySize;
                TextInputButton textInputButton = keyTransform.GetComponentInChildren<TextInputButton>();
                if (keyTransform.gameObject.name == "Padding")
                {
                    sizeDelta *= PADDING_SIZE_MULTIPLIER;
                }
                else
                {
                    switch (textInputButton.NeutralKey)
                    {
                        case KeyCode.Space:
                            sizeDelta.x = (scaledKeySize.x * SpaceSizeRelativeToKeySize) + (scaledGapSize.x * SpaceSizeRelativeToGapSize);
                            break;

                        case KeyCode.Backspace:
                            sizeDelta.x *= BackspaceSizeRelativeToKeySize;
                            break;
                        case KeyCode.RightShift:
                            sizeDelta.x *= RightShiftSizeRelativeToKeySize;
                            break;

                        case KeyCode.Return:
                            sizeDelta.x *= ReturnSizeRelativeToKeySize;
                            break;
                    }
                }
                keyTransform.sizeDelta = sizeDelta;
                MarkAsDirty(keyTransform, $"Update sizeDelta of {keyTransform.name}");

                RectTransform shadowTransform = keyboardShadowsRows[i].transform.GetChild(j).GetComponent<RectTransform>();
                shadowTransform.sizeDelta = sizeDelta;
                MarkAsDirty(shadowTransform, $"Update sizeDelta of {shadowTransform.name}");
            }
        }
    }

    // loop through each horizontal layout group & set its sizeDelta to be equal to the size of the keys & gaps inside of it
    // Set the vertical layout group to be equal to the size of its layout groups
    // Set the size of the panel to be equal to the vertical layout group + padding 
    private void SizePanel()
    {
        float longestRow = 0;
        for (int i = 0; i < keyboardKeysRows.Count; i++)
        {
            HorizontalLayoutGroup keyRow = keyboardKeysRows[i];
            HorizontalLayoutGroup shadowRow = keyboardShadowsRows.Count == 0 ? null : keyboardShadowsRows[i];

            RectTransform rowTransform = keyRow.GetComponent<RectTransform>();
            Vector2 horizontalSizeDelta = new Vector2(0, keySize / rowTransform.lossyScale.y);
            float scaledGapSize = gapSize / rowTransform.lossyScale.y;
            float currentRowLength = 0;

            foreach (RectTransform key in rowTransform)
            {
                float scaledKeySize = key.sizeDelta.x * key.lossyScale.x;
                horizontalSizeDelta.x += scaledKeySize / rowTransform.lossyScale.x;
                horizontalSizeDelta.x += scaledGapSize;

                currentRowLength += scaledKeySize;
                currentRowLength += gapSize;
            }
            horizontalSizeDelta.x -= scaledGapSize;
            horizontalSizeDelta.x += keySize / 2;
            rowTransform.sizeDelta = horizontalSizeDelta;
            MarkAsDirty(rowTransform, $"Update Size Delta of {rowTransform.name}");

            shadowRow.GetComponent<RectTransform>().sizeDelta = horizontalSizeDelta;
            MarkAsDirty(shadowRow, $"Update Size Delta of {shadowRow.name}");

            currentRowLength -= gapSize;
            longestRow = Mathf.Max(currentRowLength, longestRow);
        }

        RectTransform verticalGroup = KeyboardKeysParent.GetComponent<RectTransform>();
        Vector2 verticalSizeDelta = new Vector2()
        {
            x = longestRow / verticalGroup.lossyScale.x,
            y = ((keySize * keyboardKeysRows.Count) + (gapSize * (keyboardKeysRows.Count - 1))) / verticalGroup.lossyScale.y
        };
        verticalGroup.sizeDelta = verticalSizeDelta;
        MarkAsDirty(verticalGroup, $"Update Size Delta of {verticalGroup.name}");

        KeyboardShadowsParent.GetComponent<RectTransform>().sizeDelta = verticalSizeDelta;
        MarkAsDirty(KeyboardShadowsParent, $"Update Size Delta of {KeyboardShadowsParent.name}");

        verticalSizeDelta.x += panelPaddingRelativeToKeySize.x * (keySize / prefabParent.lossyScale.x);
        verticalSizeDelta.y += panelPaddingRelativeToKeySize.y * (keySize / prefabParent.lossyScale.y);
        prefabParent.sizeDelta = verticalSizeDelta;
        MarkAsDirty(prefabParent, $"Update Size Delta of {prefabParent.name}");
    }

    private void ResizeColliders()
    {
        List<BoxCollider> boxColliders = prefabParent.GetComponentsInChildren<BoxCollider>().ToList();
        foreach (BoxCollider boxCollider in boxColliders)
        {
            RectTransform rectTransform = boxCollider.GetComponent<RectTransform>();
            boxCollider.size = new Vector3()
            {
                x = rectTransform.rect.width,
                y = rectTransform.rect.height,
                z = colliderDepth,
            };
        }
    }

    private void MarkAsDirty(Object o, string message)
    {
#if UNITY_EDITOR
        Undo.RecordObject(o, message);
        PrefabUtility.RecordPrefabInstancePropertyModifications(o);
#endif
    }
}
