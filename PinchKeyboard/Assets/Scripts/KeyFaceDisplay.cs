#if UNITY_EDITOR
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class KeyFaceDisplay : MonoBehaviour
{
    public void Update()
    {
        transform.Find("Label").GetComponent<TextMeshPro>().text = gameObject.name[0] + "";
    }
}
#endif