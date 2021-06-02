using UnityEngine;

[ExecuteInEditMode]
public class ResizeMesh : MonoBehaviour
{
    public RectTransform parentRect;
    public float depthScale = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (parentRect == null) parentRect = GetComponentInParent<TextInputButton>().GetComponent<RectTransform>();
        UIKeyboardResizer.OnResize += UpdateSize;
    }

    void OnDisable()
    {
        UIKeyboardResizer.OnResize -= UpdateSize;
    }

    // Update is called once per frame
    void UpdateSize()
    {
        Vector3 newScale = Vector3.one;
        newScale.x = parentRect.rect.width;
        newScale.y = parentRect.rect.height;
        newScale.z = newScale.y * depthScale;

        transform.localScale = newScale;
    }
}
