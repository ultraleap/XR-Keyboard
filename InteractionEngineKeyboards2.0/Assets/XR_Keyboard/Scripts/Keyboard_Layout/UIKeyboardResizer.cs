using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardResizer : MonoBehaviour
{
    [System.Serializable]
    public struct KeyboardLayoutObjects
    {
        public RectTransform LayoutParent;
        public VerticalLayoutGroup ShadowsParent;
        public VerticalLayoutGroup KeysParent;
        [HideInInspector] public List<HorizontalLayoutGroup> ShadowsRows;
        [HideInInspector] public List<HorizontalLayoutGroup> KeysRows;
    }

    [Header("Setup")]
    public List<KeyboardLayoutObjects> keyboardLayoutObjects;

    [Header("Size (Standard Unity Units)")]
    public float gapSize = 0.005f;
    public float keySize = 0.04f;
    public Vector2 panelPaddingRelativeToKeySize = Vector2.zero * 0.2f;
    public float colliderDepth = 0.01f;

    [Header("Button Sizing")]
    public float SpaceSizeRelativeToKeySize = 9.375f;
    public float SpaceSizeRelativeToGapSize = 9f;
    public float BackspaceSizeRelativeToKeySize = 1.5f;
    public float LeftShiftSizeRelativeToKeySize = 1f;
    public float RightShiftSizeRelativeToKeySize = 1.5f;
    public float ReturnSizeRelativeToKeySize = 2f;

    [Button]
    public void ResizeKeyboard()
    {
        foreach (KeyboardLayoutObjects keyboardLayoutObject in keyboardLayoutObjects)
        {
            ResizeKeyboardLayoutObject(keyboardLayoutObject);
        }
    }

    public void ResizeKeyboardLayoutObject(KeyboardLayoutObjects keyboardLayoutObject)
    {
        keyboardLayoutObject.KeysRows = keyboardLayoutObject.KeysParent.GetComponentsInChildren<HorizontalLayoutGroup>().ToList();
        keyboardLayoutObject.ShadowsRows = keyboardLayoutObject.ShadowsParent.GetComponentsInChildren<HorizontalLayoutGroup>().ToList();

        ValidateRows(keyboardLayoutObject);
        SizeKeys(keyboardLayoutObject);
        SpaceKeyboard(keyboardLayoutObject);
        ResizeColliders(keyboardLayoutObject);
        AddPaddingToPanel(keyboardLayoutObject);
    }

    private void ValidateRows(KeyboardLayoutObjects keyboardLayoutObject)
    {


        if (keyboardLayoutObject.KeysRows.Count != keyboardLayoutObject.ShadowsRows.Count)
        {
            throw new System.Exception(
                "Keyboard Key Row Count & Keyboard Shadow Row Count are mismatched.\n"
                 + "Check that both have the same number of horizontal layout groups"
            );
        }

        for (int i = 0; i < keyboardLayoutObject.KeysRows.Count; i++)
        {
            if (keyboardLayoutObject.KeysRows[i].transform.childCount != keyboardLayoutObject.ShadowsRows[i].transform.childCount)
            {
                throw new System.Exception(
                    $"Keyboard Key Row {i} & Keyboard Shadow Row {i} have a different number of children.\n"
                     + "Ensure that every key has a matching shadow object."
                );
            }
        }
    }

    //Loop through each horizontal/vertical layout group & set the spacing to be correct 
    private void SpaceKeyboard(KeyboardLayoutObjects keyboardLayoutObject)
    {
        UpdateVerticalLayoutGroupSpacing(keyboardLayoutObject.KeysParent);
        UpdateVerticalLayoutGroupSpacing(keyboardLayoutObject.ShadowsParent);

        for (int i = 0; i < keyboardLayoutObject.KeysRows.Count; i++)
        {
            UpdateHorizontalLayoutGroupSpacing(keyboardLayoutObject.KeysRows[i]);
            UpdateHorizontalLayoutGroupSpacing(keyboardLayoutObject.ShadowsRows[i]);
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
    private void SizeKeys(KeyboardLayoutObjects keyboardLayoutObject)
    {
        for (int i = 0; i < keyboardLayoutObject.KeysRows.Count; i++)
        {
            HorizontalLayoutGroup row = keyboardLayoutObject.KeysRows[i];
            for (int j = 0; j < row.transform.childCount; j++)
            {
                RectTransform keyTransform = row.transform.GetChild(j).GetComponent<RectTransform>();
                Vector2 scaledGapSize = new Vector2(gapSize / keyTransform.transform.lossyScale.x, gapSize / keyTransform.transform.lossyScale.y);
                Vector2 scaledKeySize = new Vector2(keySize / keyTransform.transform.lossyScale.x, keySize / keyTransform.transform.lossyScale.y);

                Vector2 sizeDelta = scaledKeySize;
                TextInputButton textInputButton = keyTransform.GetComponentInChildren<TextInputButton>();
                switch (textInputButton.NeutralKey)
                {
                    case KeyCode.Space:
                        sizeDelta.x = (scaledKeySize.x * SpaceSizeRelativeToKeySize) + (scaledGapSize.x * SpaceSizeRelativeToGapSize);
                        break;

                    case KeyCode.Backspace:
                        sizeDelta.x *= BackspaceSizeRelativeToKeySize;
                        break;

                    case KeyCode.LeftShift:
                        sizeDelta.x *= LeftShiftSizeRelativeToKeySize;
                        break;

                    case KeyCode.RightShift:
                        sizeDelta.x *= RightShiftSizeRelativeToKeySize;
                        break;

                    case KeyCode.Return:
                        sizeDelta.x *= ReturnSizeRelativeToKeySize;
                        break;
                }

                keyTransform.sizeDelta = sizeDelta;
                MarkAsDirty(keyTransform, $"Update sizeDelta of {keyTransform.name}");

                RectTransform shadowTransform = keyboardLayoutObject.ShadowsRows[i].transform.GetChild(j).GetComponent<RectTransform>();
                shadowTransform.sizeDelta = sizeDelta;
                MarkAsDirty(shadowTransform, $"Update sizeDelta of {shadowTransform.name}");
            }
        }
    }

    private void AddPaddingToPanel(KeyboardLayoutObjects keyboardLayoutObject)
    {
        keyboardLayoutObject.KeysParent.padding = new RectOffset()
        {
            left = (int)(panelPaddingRelativeToKeySize.x * (keySize / keyboardLayoutObject.KeysParent.transform.lossyScale.x)),
            right = (int)(panelPaddingRelativeToKeySize.x * (keySize / keyboardLayoutObject.KeysParent.transform.lossyScale.x)),
            top = (int)(panelPaddingRelativeToKeySize.y * (keySize / keyboardLayoutObject.KeysParent.transform.lossyScale.y)),
            bottom = (int)(panelPaddingRelativeToKeySize.y * (keySize / keyboardLayoutObject.KeysParent.transform.lossyScale.y)),
        };
        MarkAsDirty(keyboardLayoutObject.KeysParent, $"Update padding of {keyboardLayoutObject.KeysParent.name}");
        keyboardLayoutObject.ShadowsParent.padding = keyboardLayoutObject.KeysParent.padding;
        MarkAsDirty(keyboardLayoutObject.ShadowsParent, $"Update padding of {keyboardLayoutObject.ShadowsParent.name}");
        Canvas.ForceUpdateCanvases();

        RectTransform KeysParentRectTransform = keyboardLayoutObject.KeysParent.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(KeysParentRectTransform);
        keyboardLayoutObject.LayoutParent.sizeDelta = KeysParentRectTransform.sizeDelta;
        MarkAsDirty(keyboardLayoutObject.LayoutParent, $"Update sizeDelta of {keyboardLayoutObject.LayoutParent.name}");
    }

    private void ResizeColliders(KeyboardLayoutObjects keyboardLayoutObject)
    {
        List<BoxCollider> boxColliders = keyboardLayoutObject.LayoutParent.GetComponentsInChildren<BoxCollider>().ToList();
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
