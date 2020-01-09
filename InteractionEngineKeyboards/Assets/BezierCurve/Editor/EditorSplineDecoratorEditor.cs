using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditorSplineDecorator))]
public class EditorSplineDecoratorEditor : Editor {

	public override void OnInspectorGUI () {
        if (GUILayout.Button("Align"))
        {
            EditorSplineDecorator esd = (EditorSplineDecorator)serializedObject.targetObject;
            esd.AlignObjects();
        }
        DrawDefaultInspector();
	}
}