using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetChildImageColour : MonoBehaviour
{
    public Color ShadowColour;

    [Button]
    private void SetChildImageColours()
    {
        List<Image> images = transform.GetComponentsInChildren<Image>().ToList();
        foreach (Image image in images)
        {
            image.color = ShadowColour;
            MarkAsDirty(image, $"Set Image: {image.name}'s colour");
        }
    }

    private void MarkAsDirty(UnityEngine.Object o, string message)
    {
#if UNITY_EDITOR
        Undo.RecordObject(o, message);
        PrefabUtility.RecordPrefabInstancePropertyModifications(o);
#endif
    }
}
