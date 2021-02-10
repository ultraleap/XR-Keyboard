using System;
using System.Linq;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static KeyboardManager;

public class UITextInputButton : TextInputButton
{
    Button button;
    TextMeshProUGUI uiTextMesh;
    public override void Start()
    {
        uiTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TextPress);
        base.Start();
    }

    protected override void UpdateKeyText(string text)
    {
        uiTextMesh.text = text;  
    }
}
