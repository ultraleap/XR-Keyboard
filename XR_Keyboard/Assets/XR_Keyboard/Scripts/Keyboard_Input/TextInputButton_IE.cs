using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Interaction.Keyboard
{
    public class TextInputButton_IE : TextInputButton
    {
        public InteractionButton interactionButton;

        protected override void Awake()
        {
            if (interactionButton == null)
            {
                interactionButton = GetComponentInChildren<InteractionButton>();
            }

            if (interactionButton != null)
            {
                interactionButton.OnPress += LongPressStart;
                interactionButton.OnUnpress += TextPress;
            }

            base.Awake();
        }

        void Update()
        {
            isPressed = interactionButton.isPressed;
        }

        protected override void UpdateKeyState(string text)
        {

            bool enabled = text.Length > 0;
            foreach (var image in GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                image.enabled = enabled;
            }


            if (interactionButton != null)
            {
                if (text == "")
                {
                    interactionButton.controlEnabled = false;
                }
                else
                {
                    interactionButton.controlEnabled = enabled;
                }
            }

            base.UpdateKeyState(text);
        }
    }
}