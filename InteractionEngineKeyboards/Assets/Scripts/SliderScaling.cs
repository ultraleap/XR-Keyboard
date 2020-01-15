using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScaling : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = target.transform.localScale;
    }

    public void ApplySliderScale(float newScale)
    {
        target.transform.localScale = new Vector3()
        {
            x = (newScale * initialScale.x),
            y = (newScale * initialScale.y),
            z = (newScale * initialScale.z)
        };
    }
}
