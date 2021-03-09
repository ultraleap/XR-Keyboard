using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class GapSizeOutput : MonoBehaviour
{
    public InteractionButtonKeyboardResizer keyboardPositioner;
    public TextMeshPro gapSizeValues;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gapSizeValues.text = $"Row:\t{keyboardPositioner.buttonGapRow * 100:0.00}cm\nCol:\t{keyboardPositioner.buttonGapColumn * 100:0.00}cm";
    }
}
