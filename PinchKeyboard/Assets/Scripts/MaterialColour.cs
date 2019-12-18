using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialColour : MonoBehaviour
{

    [SerializeField] Color color = new Color();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnValidate()
    {
        Renderer[] kids = transform.GetComponentsInChildren<Renderer>();

        foreach (Renderer kid in kids)
        {
            Material m = new Material(kid.sharedMaterial);
            m.SetColor("_Color", color);
            kid.sharedMaterial = m;
        }
    }
}
