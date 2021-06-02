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
    public VerticalLayoutGroup keyboardLayoutObjectsParent;

    [Header("Size (Standard Unity Units)")]
    public float gapSize = 0.005f;
    public float keySize = 0.04f;
    public Vector2 panelPaddingRelativeToKeySize = Vector2.zero * 0.2f;
    public float colliderDepth = 0.01f;

    [Header("Button Sizing")]
    public float SpacingBetweenKeyboardLayoutObjectsRelativeToKeySize = 0.25f;
    public delegate void Resizing();
    public static event Resizing OnResize;

    [Button]
    public void ResizeKeyboard()
    {
        foreach (KeyboardLayoutObjects keyboardLayoutObject in keyboardLayoutObjects)
        {
            ResizeKeyboardLayoutObject(keyboardLayoutObject);
        }
        ResizeKeyboardLayoutObjectsParentSpacing();
        OnResize?.Invoke();
    }

#if UNITY_EDITOR
    // Unity complains if we rebuild in OnValidate, so rebuild just after to ensure that the layout groups are correct
    // Thanks to this thread for this solution:
    // https://forum.unity.com/threads/sendmessage-cannot-be-called-during-awake-checkconsistency-or-onvalidate-can-we-suppress.537265/
    void OnValidate() { EditorApplication.delayCall += _OnValidate; }
    private void _OnValidate()
    {
        foreach (KeyboardLayoutObjects keyboardLayoutObject in keyboardLayoutObjects)
        {
            if (keyboardLayoutObject.KeysParent != null)
            {
                RectTransform keysParent = keyboardLayoutObject.KeysParent.GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(keysParent);
            }
            if (keyboardLayoutObject.ShadowsParent != null)
            {
                RectTransform shadowsParent = keyboardLayoutObject.ShadowsParent.GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(shadowsParent);
            }
        }
        Canvas.ForceUpdateCanvases();
    }
#endif
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
                sizeDelta.x *= textInputButton.GetKeyScale();

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

        //Force Canvas & Rect Transform to rebuild to enable content size fitter components to resize KeysParent's RectTransform, so that we can copy its size delta 
        RectTransform KeysParentRectTransform = keyboardLayoutObject.KeysParent.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(KeysParentRectTransform);
        Canvas.ForceUpdateCanvases();

        Vector2 normalisedSizeDelta = new Vector2()
        {
            x = KeysParentRectTransform.sizeDelta.x * KeysParentRectTransform.lossyScale.x,
            y = KeysParentRectTransform.sizeDelta.y * KeysParentRectTransform.lossyScale.y,
        };

        keyboardLayoutObject.LayoutParent.sizeDelta = normalisedSizeDelta / keyboardLayoutObject.LayoutParent.lossyScale;
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

    private void ResizeKeyboardLayoutObjectsParentSpacing()
    {
        if (keyboardLayoutObjectsParent == null)
        {
            return;
        }
        keyboardLayoutObjectsParent.spacing = SpacingBetweenKeyboardLayoutObjectsRelativeToKeySize * (keySize / keyboardLayoutObjectsParent.transform.lossyScale.y);
        LayoutRebuilder.ForceRebuildLayoutImmediate(keyboardLayoutObjectsParent.GetComponent<RectTransform>());
    }

    private void MarkAsDirty(Object o, string message)
    {
#if UNITY_EDITOR
        Undo.RecordObject(o, message);
        PrefabUtility.RecordPrefabInstancePropertyModifications(o);
#endif
    }
}
