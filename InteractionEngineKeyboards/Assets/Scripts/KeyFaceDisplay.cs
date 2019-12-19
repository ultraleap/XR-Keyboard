#if UNITY_EDITOR
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class KeyFaceDisplay : MonoBehaviour
{
    public void Update()
    {
        if (Application.isPlaying) return;

        GetComponentInChildren<TextMeshPro>().text = gameObject.name[0] + "";
    }
}
#endif