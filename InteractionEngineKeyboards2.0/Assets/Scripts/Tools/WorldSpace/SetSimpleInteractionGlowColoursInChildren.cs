using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class SetSimpleInteractionGlowColoursInChildren : MonoBehaviour
{
    public Color TextColor = Color.Lerp(Color.black, Color.white, 0.2F);
    [Header("InteractionBehaviour Colors")]
    public Color defaultColor = Color.Lerp(Color.black, Color.white, 0.1F);
    public Color suspendedColor = Color.red;
    public Color hoverColor = Color.Lerp(Color.black, Color.white, 0.7F);
    public Color primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8F);

    public bool UseHover = false;
    public bool UsePrimaryHover = true;

    [Header("InteractionButton Colors")]
    [Tooltip("This color only applies if the object is an InteractionButton or InteractionSlider.")]
    public Color pressedColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        SetColours();
    }

    [Button]
    private void SetColours()
    {
        SimpleInteractionGlow[] simpleInteractionGlows = transform.GetComponentsInChildren<SimpleInteractionGlow>(true);
        foreach (SimpleInteractionGlow simpleInteractionGlow in simpleInteractionGlows)
        {
            simpleInteractionGlow.defaultColor = defaultColor;
            simpleInteractionGlow.suspendedColor = suspendedColor;
            simpleInteractionGlow.hoverColor = hoverColor;
            simpleInteractionGlow.primaryHoverColor = primaryHoverColor;
            simpleInteractionGlow.pressedColor = pressedColor;
            simpleInteractionGlow.useHover = UseHover;
            simpleInteractionGlow.usePrimaryHover = UsePrimaryHover;
        }

        SimpleInteractionGlowImage[] SimpleInteractionGlowImages = transform.GetComponentsInChildren<SimpleInteractionGlowImage>(true);
        foreach (SimpleInteractionGlowImage simpleInteractionGlowImage in SimpleInteractionGlowImages)
        {
            simpleInteractionGlowImage.defaultColor = defaultColor;
            simpleInteractionGlowImage.suspendedColor = suspendedColor;
            simpleInteractionGlowImage.hoverColor = hoverColor;
            simpleInteractionGlowImage.primaryHoverColor = primaryHoverColor;
            simpleInteractionGlowImage.pressedColor = pressedColor;
            simpleInteractionGlowImage.useHover = UseHover;
            simpleInteractionGlowImage.usePrimaryHover = UsePrimaryHover;
        }

        transform.GetComponents<TextMeshPro>().ToList().ForEach(tmp => tmp.color = TextColor);
        transform.GetComponents<TextMeshProUGUI>().ToList().ForEach(tmp => tmp.color = TextColor);
    }
}
