using Leap.Unity.Interaction.PhysicsHands;

namespace Leap.Unity.Interaction.Keyboard
{
    public class TextInputButton_PhysicsButton : TextInputButton
    {
        public PhysicsButton physicsButton;

        protected override void Awake()
        {
            if (physicsButton == null)
            {
                physicsButton = GetComponentInChildren<PhysicsButton>();
            }

            if (physicsButton != null)
            {
                physicsButton.OnPress += LongPressStart;
                physicsButton.OnUnpress += TextPress;
            }

            base.Awake();
        }

        void Update()
        {
            isPressed = physicsButton.isPressed;
        }

        protected override void UpdateKeyState(string text)
        {

            bool enabled = text.Length > 0;
            foreach (var image in GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                image.enabled = enabled;
            }


            if (physicsButton != null)
            {
                if (text == "")
                {
                    physicsButton.enabled = false;
                }
                else
                {
                    physicsButton.enabled = enabled;
                }
            }

            base.UpdateKeyState(text);
        }
    }
}
