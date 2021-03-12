using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using static SimpleInteractionGlowImage;

public class SetSimpleInteractionGlowColoursInChildren : MonoBehaviour
{
    public Color TextColor = Color.Lerp(Color.black, Color.white, 0.2F);
    public InteractionBehaviourColours colors = new InteractionBehaviourColours()
    {
        defaultColor = Color.Lerp(Color.black, Color.white, 0.1F),
        suspendedColor = Color.red,
        hoverColor = Color.Lerp(Color.black, Color.white, 0.7F),
        primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8F),
        pressedColor = Color.white
    };


    public bool UseHover = false;
    public bool UsePrimaryHover = true;


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
            simpleInteractionGlow.defaultColor = colors.defaultColor;
            simpleInteractionGlow.suspendedColor = colors.suspendedColor;
            simpleInteractionGlow.hoverColor = colors.hoverColor;
            simpleInteractionGlow.primaryHoverColor = colors.primaryHoverColor;
            simpleInteractionGlow.pressedColor = colors.pressedColor;
            simpleInteractionGlow.useHover = UseHover;
            simpleInteractionGlow.usePrimaryHover = UsePrimaryHover;
        }

        SimpleInteractionGlowImage[] SimpleInteractionGlowImages = transform.GetComponentsInChildren<SimpleInteractionGlowImage>(true);
        foreach (SimpleInteractionGlowImage simpleInteractionGlowImage in SimpleInteractionGlowImages)
        {
            simpleInteractionGlowImage.colors = colors;
            simpleInteractionGlowImage.usePrimaryHover = UsePrimaryHover;
        }

        transform.GetComponents<TextMeshPro>().ToList().ForEach(tmp => tmp.color = TextColor);
        transform.GetComponents<TextMeshProUGUI>().ToList().ForEach(tmp => tmp.color = TextColor);
    }
}
