/******************************************************************************
 * Copyright (C) Ultraleap, Inc. 2011-2020.                                   *
 *                                                                            *
 * Use subject to the terms of the Apache License 2.0 available at            *
 * http://www.apache.org/licenses/LICENSE-2.0, or another agreement           *
 * between Ultraleap and you, your company or other organization.             *
 ******************************************************************************/

using System;
using UnityEngine;

namespace Leap.Unity.Interaction.Keyboard
{

    /// <summary>
    /// This simple script changes the color of an InteractionBehaviour as
    /// a function of its distance to the palm of the closest hand that is
    /// hovering nearby.
    /// </summary>
    [AddComponentMenu("")]
    [RequireComponent(typeof(InteractionBehaviour))]
    public class SimpleInteractionGlowImage : MonoBehaviour
    {
        [Serializable]
        public struct InteractionBehaviourColours
        {
            [Header("InteractionBehaviour Colors")]
            public Color defaultColor;
            public Color suspendedColor;
            public Color hoverColor;
            public Color primaryHoverColor;

            [Header("InteractionButton Colors")]
            [Tooltip("This color only applies if the object is an InteractionButton or InteractionSlider.")]
            public Color pressedColor;
        }

        [Tooltip("If enabled, the object will lerp to its hoverColor when a hand is nearby.")]
        public bool useHover = true;

        [Tooltip("If enabled, the object will use its primaryHoverColor when the primary hover of an InteractionHand.")]
        public bool usePrimaryHover = false;

        public InteractionBehaviourColours colors = new InteractionBehaviourColours()
        {
            defaultColor = Color.Lerp(Color.black, Color.white, 0.1F),
            suspendedColor = Color.red,
            hoverColor = Color.Lerp(Color.black, Color.white, 0.7F),
            primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8F),
            pressedColor = Color.white
        };
        private UnityEngine.UI.Image image;

        private InteractionBehaviour _intObj;

        void Start()
        {
            _intObj = GetComponent<InteractionBehaviour>();

            image = GetComponent<UnityEngine.UI.Image>();
            if (image == null)
            {
                image = GetComponentInChildren<UnityEngine.UI.Image>();
            }
        }

        void Update()
        {
            if (image != null)
            {

                // The target color for the Interaction object will be determined by various simple state checks.
                Color targetColor = colors.defaultColor;

                // "Primary hover" is a special kind of hover state that an InteractionBehaviour can
                // only have if an InteractionHand's thumb, index, or middle finger is closer to it
                // than any other interaction object.
                if (_intObj.isPrimaryHovered && usePrimaryHover)
                {
                    targetColor = colors.primaryHoverColor;
                }
                else
                {
                    // Of course, any number of objects can be hovered by any number of InteractionHands.
                    // InteractionBehaviour provides an API for accessing various interaction-related
                    // state information such as the closest hand that is hovering nearby, if the object
                    // is hovered at all.
                    if (_intObj.isHovered && useHover)
                    {
                        float glow = _intObj.closestHoveringControllerDistance.Map(0F, 0.2F, 1F, 0.0F);
                        targetColor = Color.Lerp(colors.defaultColor, colors.hoverColor, glow);
                    }
                }

                if (_intObj.isSuspended)
                {
                    // If the object is held by only one hand and that holding hand stops tracking, the
                    // object is "suspended." InteractionBehaviour provides suspension callbacks if you'd
                    // like the object to, for example, disappear, when the object is suspended.
                    // Alternatively you can check "isSuspended" at any time.
                    targetColor = colors.suspendedColor;
                }

                // We can also check the depressed-or-not-depressed state of InteractionButton objects
                // and assign them a unique color in that case.
                if (_intObj is InteractionButton && (_intObj as InteractionButton).isPressed)
                {
                    targetColor = colors.pressedColor;
                }

                // Lerp actual material color to the target color.
                image.color = Color.Lerp(image.color, targetColor, 30F * Time.deltaTime);
            }
        }
    }
}
