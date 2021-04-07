using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardResizer : MonoBehaviour
{
    [Header("Setup")]
    public VerticalLayoutGroup KeyboardKeysParent;
    public VerticalLayoutGroup KeyboardShadowsParent;
    public RectTransform prefabParent;
    private List<HorizontalLayoutGroup> keyboardKeysRows;
    private List<HorizontalLayoutGroup> keyboardShadowsRows;

    [Header("Size")]
    public float gapSize = 0.005f;
    public float keySize = 0.04f;
    public Vector2 panelPaddingRelativeToKeySize = Vector2.zero * 0.5f;
    public float colliderDepth = 0.01f;

    [Header("Button Sizing")]
    public float SpaceSizeRelativeToKeySize = 9.375f;
    public float SpaceSizeRelativeToGapSize = 9f;
    public float BackspaceSizeRelativeToKeySize = 1.5f;
    public float RightShiftSizeRelativeToKeySize = 1.5f;
    public float ReturnSizeRelativeToKeySize = 2f;
    private const float PADDING_SIZE_MULTIPLIER = 0.5f;

    public void ResizeKeyboard()
    {

        ValidateRows();
    }

    public void ResizeButtons()
    {
        ValidateRows();
        SizeKeys();
        SpaceKeyboard();
        ResizeColliders();
    }

    public void ResizePadding()
    {
        ValidateRows();
        AddPaddingToPanel();
    }

    private void ValidateRows()
    {
        keyboardKeysRows = KeyboardKeysParent.GetComponentsInChildren<HorizontalLayoutGroup>().ToList();
        keyboardShadowsRows = KeyboardShadowsParent.GetComponentsInChildren<HorizontalLayoutGroup>().ToList();
        
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

    private void AddPaddingToPanel()
    {
        KeyboardKeysParent.padding = new RectOffset()
        {
            left = (int)(panelPaddingRelativeToKeySize.x * (keySize / KeyboardKeysParent.transform.lossyScale.x)),
            right = (int)(panelPaddingRelativeToKeySize.x * (keySize / KeyboardKeysParent.transform.lossyScale.x)),
            top = (int)(panelPaddingRelativeToKeySize.y * (keySize / KeyboardKeysParent.transform.lossyScale.y)),
            bottom = (int)(panelPaddingRelativeToKeySize.y * (keySize / KeyboardKeysParent.transform.lossyScale.y)),
        };
        MarkAsDirty(KeyboardKeysParent, $"Update padding of {KeyboardKeysParent.name}");
        KeyboardShadowsParent.padding = KeyboardKeysParent.padding;
        MarkAsDirty(KeyboardShadowsParent, $"Update padding of {KeyboardShadowsParent.name}");
    }

    public void UpdatePrefabParentSize()
    {
        if (prefabParent.sizeDelta != KeyboardKeysParent.GetComponent<RectTransform>().sizeDelta)
        {
            prefabParent.sizeDelta = KeyboardKeysParent.GetComponent<RectTransform>().sizeDelta;
        }
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
