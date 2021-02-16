using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity.Interaction;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardResizer : MonoBehaviour
{

    [BoxGroup("Setup")] public VerticalLayoutGroup KeyboardKeysParent;
    [BoxGroup("Setup")] public VerticalLayoutGroup KeyboardShadowsParent;
    [BoxGroup("Setup")] public RectTransform prefabParent;
    [BoxGroup("Setup")] public List<HorizontalLayoutGroup> keyboardRows;
    [BoxGroup("Setup")] public List<HorizontalLayoutGroup> keyboardRowShadows;

    [BoxGroup("Size")] public float gapSize;
    [BoxGroup("Size")] public float buttonSize;
    [BoxGroup("Size")] public Vector2 panelPaddingRelativeToButtonSize = Vector2.zero;
    [BoxGroup("Size")] public float colliderDepth = 0.01f;

    [Button]
    private void ResizeKeyboard()
    {
        EnableLayoutGroups(true);
        SpaceKeyboard();
        SizeButtons();
        SizePanel();
        ResizeColliders();
    }

    [Button]
    private void DisableLayoutGroups()
    {
        EnableLayoutGroups(false);
    }

    private void EnableLayoutGroups(bool _enable)
    {
        KeyboardKeysParent.enabled = _enable;
        keyboardRows.ForEach(kr => kr.enabled = _enable);
    }

    private void SizePanel()
    {
        float longestRow = 0;
        for (int i = 0; i < keyboardRows.Count; i++)
        {
            HorizontalLayoutGroup row = keyboardRows[i];
            HorizontalLayoutGroup shadowRow = keyboardRowShadows.Count == 0 ? null : keyboardRowShadows[i];

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

            if (shadowRow != null)
            {
                shadowRow.GetComponent<RectTransform>().sizeDelta = horizontalSizeDelta;
                MarkAsDirty(shadowRow, $"Update Size Delta of {shadowRow.name}");
            }
            currentRowLength -= gapSize;
            longestRow = Mathf.Max(currentRowLength, longestRow);
            Leap.Finger finger;
        }

        RectTransform verticalGroup = KeyboardKeysParent.GetComponent<RectTransform>();
        Vector2 verticalSizeDelta = new Vector2()
        {
            x = longestRow / verticalGroup.lossyScale.x,
            y = ((buttonSize * keyboardRows.Count) + (gapSize * (keyboardRows.Count - 1))) / verticalGroup.lossyScale.y
        };
        verticalGroup.sizeDelta = verticalSizeDelta;
        MarkAsDirty(verticalGroup, $"Update Size Delta of {verticalGroup.name}");

        if (KeyboardShadowsParent != null)
        {
            KeyboardShadowsParent.GetComponent<RectTransform>().sizeDelta = verticalSizeDelta;
            MarkAsDirty(KeyboardShadowsParent, $"Update Size Delta of {KeyboardShadowsParent.name}");
        }

        verticalSizeDelta.x += panelPaddingRelativeToButtonSize.x * (buttonSize / prefabParent.lossyScale.x);
        verticalSizeDelta.y += panelPaddingRelativeToButtonSize.y * (buttonSize / prefabParent.lossyScale.y);
        prefabParent.sizeDelta = verticalSizeDelta;
        MarkAsDirty(prefabParent, $"Update Size Delta of {prefabParent.name}");
    }

    private void SpaceKeyboard()
    {
        KeyboardKeysParent.spacing = gapSize / KeyboardKeysParent.transform.lossyScale.y;
        MarkAsDirty(KeyboardKeysParent, $"Update spacing of {KeyboardKeysParent.name}");

        if (KeyboardShadowsParent != null)
        {
            KeyboardShadowsParent.spacing = gapSize / KeyboardShadowsParent.transform.lossyScale.y;
            MarkAsDirty(KeyboardShadowsParent, $"Update spacing of {KeyboardShadowsParent.name}");
        }

        for (int i = 0; i < keyboardRows.Count; i++)
        {
            HorizontalLayoutGroup horizontalLayoutGroup = keyboardRows[i];
            horizontalLayoutGroup.spacing = gapSize / horizontalLayoutGroup.transform.lossyScale.x;
            MarkAsDirty(horizontalLayoutGroup, $"Update spacing of {horizontalLayoutGroup.name}");

            if (keyboardRowShadows.Count == keyboardRows.Count)
            {
                horizontalLayoutGroup = keyboardRowShadows[i];
                horizontalLayoutGroup.spacing = gapSize / horizontalLayoutGroup.transform.lossyScale.x;
                MarkAsDirty(horizontalLayoutGroup, $"Update spacing of {horizontalLayoutGroup.name}");
            }
        }
    }

    private void SizeButtons()
    {
        for (int i = 0; i < keyboardRows.Count; i++)
        {
            HorizontalLayoutGroup row = keyboardRows[i];
            for (int j = 0; j < row.transform.childCount; j++)
            {
                RectTransform buttonTransform = row.transform.GetChild(j).GetComponent<RectTransform>();
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

                if (keyboardRowShadows.Count == keyboardRows.Count)
                {
                    RectTransform buttonShadow = keyboardRowShadows[i].transform.GetChild(j).GetComponent<RectTransform>();
                    buttonShadow.sizeDelta = sizeDelta;
                    MarkAsDirty(buttonShadow, $"Update sizeDelta of {buttonShadow.name}");
                }
            }
        }
    }

    private void MarkAsDirty(UnityEngine.Object o, string message)
    {
        Undo.RecordObject(o, message);
        PrefabUtility.RecordPrefabInstancePropertyModifications(o);
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

    private void SetLayoutGroupsActive(bool _layoutGroupsActive)
    {
        List<InteractionButton> interactionButtons = prefabParent.GetComponentsInChildren<InteractionButton>().ToList();
        interactionButtons.ForEach(ib => ib.enabled = !_layoutGroupsActive);
        keyboardRows.ForEach(row => row.enabled = _layoutGroupsActive);
        KeyboardKeysParent.enabled = _layoutGroupsActive;
    }

    private IEnumerator SetLayoutGroupsActiveAfterNSeconds(bool _layoutGroupsActive, float _duration)
    {
        yield return new WaitForSeconds(_duration);
        SetLayoutGroupsActive(_layoutGroupsActive);
    }
}
