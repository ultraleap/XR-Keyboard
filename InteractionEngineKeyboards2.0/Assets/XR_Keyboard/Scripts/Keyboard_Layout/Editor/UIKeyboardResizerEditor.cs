using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIKeyboardResizer))]
public class UIKeyboardResizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIKeyboardResizer uiKeyboardResizer = (UIKeyboardResizer)target;

        uiKeyboardResizer.UpdatePrefabParentSize();

        if (GUILayout.Button("Resize Keyboard"))
        {
            uiKeyboardResizer.ResizeButtons();
            uiKeyboardResizer.ResizePadding();
        }
    }
}