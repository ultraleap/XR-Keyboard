
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Leap.Unity.Interaction.Keyboard
{
    public class TextInputButton_UI : TextInputButton
    {
        public Button button;

        protected override void Awake()
        {

            if (button == null)
            {
                button = GetComponentInChildren<Button>();
            }

            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    isPressed = false;
                    TextPress();
                }
                );
            }

            base.Awake();
        }

        void Update()
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LongPressStart();
            isPressed = true;
        }

        protected override void UpdateKeyState(string text)
        {

            bool enabled = text.Length > 0;
            foreach (var image in GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                image.enabled = enabled;
            }


            if (button != null)
            {
                if (text == "")
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = enabled;
                }
            }

            base.UpdateKeyState(text);
        }
    }
}
