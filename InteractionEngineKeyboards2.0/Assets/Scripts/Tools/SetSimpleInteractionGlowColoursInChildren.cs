using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSimpleInteractionGlowColoursInChildren : MonoBehaviour
{
    [Header("InteractionBehaviour Colors")]
    public Color defaultColor = Color.Lerp(Color.black, Color.white, 0.1F);
    public Color suspendedColor = Color.red;
    public Color hoverColor = Color.Lerp(Color.black, Color.white, 0.7F);
    public Color primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8F);

    [Header("InteractionButton Colors")]
    [Tooltip("This color only applies if the object is an InteractionButton or InteractionSlider.")]
    public Color pressedColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        SimpleInteractionGlow[] simpleInteractionGlows = transform.GetComponentsInChildren<SimpleInteractionGlow>(true);
        foreach (SimpleInteractionGlow simpleInteractionGlow in simpleInteractionGlows)
        {
            simpleInteractionGlow.defaultColor = defaultColor;
            simpleInteractionGlow.suspendedColor = suspendedColor;
            simpleInteractionGlow.hoverColor = hoverColor;
            simpleInteractionGlow.primaryHoverColor = primaryHoverColor;
            simpleInteractionGlow.pressedColor = pressedColor;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
