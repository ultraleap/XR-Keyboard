using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[ExecuteInEditMode]
public class KeyResizer : MonoBehaviour
{
    public List<RectTransform> buttons;
    public HorizontalLayoutGroup[] layoutGroupsH;
    public VerticalLayoutGroup mainPanel;
    public Vector2 buttonSize;

    public Vector2 buttonSpacing;

    public  int panelPadding;

    // Start is called before the first frame update
    void OnEnable()
    {
        Button[] uiButtons = GetComponentsInChildren<Button>();
        buttons = new List<RectTransform>();

        foreach(var but in uiButtons)
        {
            buttons.Add(but.GetComponent<RectTransform>());
        }

        layoutGroupsH = GetComponentsInChildren<HorizontalLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void ResizeButtons()
    {
        foreach(var but in buttons)
        {
            but.sizeDelta = buttonSize;
        }

        foreach(var group in layoutGroupsH)
        {
            group.spacing = buttonSpacing.x;
        }

        mainPanel.padding.left = panelPadding;
        mainPanel.padding.right = panelPadding;
        mainPanel.padding.top = panelPadding;
        mainPanel.padding.bottom = panelPadding;
    }
}
