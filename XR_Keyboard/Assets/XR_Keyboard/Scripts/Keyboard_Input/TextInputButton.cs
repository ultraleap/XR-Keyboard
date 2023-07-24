﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KeyboardMode = Leap.Unity.Interaction.Keyboard.Keyboard.KeyboardMode;

namespace Leap.Unity.Interaction.Keyboard
{
    public class TextInputButton : MonoBehaviour
    {
        public delegate void KeyUp(string key, Keyboard sourceKeyboard);
        public static event KeyUp HandleKeyUp;
        public delegate void LongPress(List<string> accentedChars, Keyboard sourceKeyboard, Transform transform);
        public static event LongPress HandleLongPress;
        public string Key;
        public float keyWidthScale = 1;
        public float longPressTime = 0.75f;
        private TextMeshPro keyTextMesh;
        private TextMeshProUGUI keyTextMeshGUI;
        private TextMeshProUGUI accentLabelTextMeshGUI;
        private IEnumerator LongPressDetectorCoroutine, LongPressCoroutine;
        private Keyboard parentKeyboard;
        private bool longPressed = false;
        private Color pressedColour;

        protected bool isPressed = false;

        // Start is called before the first frame update
        protected virtual void Awake()
        {
            parentKeyboard = GetComponentInParent<Keyboard>();
            UpdateActiveKey(Key, KeyboardMode.NEUTRAL);
        }

        public void UpdateActiveKey(string _key, KeyboardMode keyboardMode)
        {
            if (keyTextMesh == null)
            {
                keyTextMesh = transform.GetComponentInChildren<TextMeshPro>();
            }

            var textMeshGUIs = transform.GetComponentsInChildren<TextMeshProUGUI>();
            if (keyTextMeshGUI == null && textMeshGUIs.Length > 0)
            {
                keyTextMeshGUI = textMeshGUIs[0];
            }

            if (accentLabelTextMeshGUI == null && textMeshGUIs.Length > 1)
            {
                // TODO, how to do this better?
                accentLabelTextMeshGUI = textMeshGUIs[1];
            }

            Key = _key;
            string keyCodeText = keyboardMode == KeyboardMode.SHIFT || keyboardMode == KeyboardMode.CAPS ? Key.ToUpper() : Key.ToLower();

            string displayStringKey = Key;
            if (displayStringKey == "shift")
            {
                if (keyboardMode == KeyboardMode.NEUTRAL)
                {
                    displayStringKey += "_neutral";
                }
                else if (keyboardMode == KeyboardMode.SHIFT)
                {
                    displayStringKey += "_shift";
                }
                else if (keyboardMode == KeyboardMode.CAPS)
                {
                    displayStringKey += "_caps";
                }
            }

            if (KeyboardCollections.NonStandardKeyToDisplayString.TryGetValue(displayStringKey, out string nonStandardKeyCodeText))
            {
                keyCodeText = nonStandardKeyCodeText;
            }

            UpdateKeyState(keyCodeText);

            if (accentLabelTextMeshGUI != null)
            {
                if (parentKeyboard == null) parentKeyboard = GetComponentInParent<Keyboard>();
                accentLabelTextMeshGUI.text = KeyboardCollections.CharacterToAccentedChars.ContainsKey(Key) ? parentKeyboard.LongPressIndicator : "";
            }
        }

        protected virtual void UpdateKeyState(string text)
        {
            UpdateKeyText(text);
        }

        protected virtual void UpdateKeyText(string text)
        {
            if (keyTextMesh != null) { keyTextMesh.text = text; }
            if (keyTextMeshGUI != null) { keyTextMeshGUI.text = text; }
        }

        public void TextPress()
        {
            if (!longPressed)
            {
                KeyUpEvent();
            }
            else
            {
                longPressed = false;
            }
        }

        protected void LongPressStart()
        {
            isPressed = true;
            longPressed = false;
            LongPressDetectorCoroutine = LongpressDetection();
            StartCoroutine(LongPressDetectorCoroutine);
        }

        private IEnumerator LongpressDetection()
        {
            float longpressThreshold = Time.time + longPressTime;
            while (isPressed && !longPressed)
            {
                if (Time.time > longpressThreshold)
                {
                    InvokeLongPress();
                }
                yield return null;
            }
        }

        private void InvokeLongPress()
        {

            switch (Key)
            {
                case "backspace":
                 //   pressedColour = GetComponentInChildren<SimpleInteractionGlowImage>().colors.pressedColor;
                //    GetComponentInChildren<SimpleInteractionGlowImage>().colors.pressedColor = parentKeyboard.LongPressColour;
                    StartCoroutine("LongPressColourSwap");

                    LongPressCoroutine = BackspaceLongPress();
                    StartCoroutine(LongPressCoroutine);
                    longPressed = true;
                    break;
                default:
                    if (KeyboardCollections.CharacterToAccentedChars.ContainsKey(Key))
                    {
                      //  pressedColour = GetComponentInChildren<SimpleInteractionGlowImage>().colors.pressedColor;
                     //   GetComponentInChildren<SimpleInteractionGlowImage>().colors.pressedColor = parentKeyboard.LongPressColour;
                        StartCoroutine("LongPressColourSwap");

                        HandleLongPress?.Invoke(KeyboardCollections.CharacterToAccentedChars[Key], parentKeyboard, transform);
                        longPressed = true;
                    }
                    break;
            }
        }

        private IEnumerator BackspaceLongPress()
        {
            float gracePeriodThreshold = Time.time + parentKeyboard.BackspaceLongpressGracePeriod;
            while (Time.time < gracePeriodThreshold)
            {
                if (!isPressed)
                {
                    break;
                }
                yield return null;
            }


            float timeStep = 0.1f;
            float nextPress = 0;
            while (isPressed)
            {
                if (Time.time > nextPress)
                {
                    nextPress = Time.time + timeStep;
                    KeyUpEvent();
                }
                yield return null;
            }
        }



        private IEnumerator LongPressColourSwap()
        {
            while (isPressed)
            {
                yield return null;
            }
          //  GetComponentInChildren<SimpleInteractionGlowImage>().colors.pressedColor = pressedColour;
        }

        private void KeyUpEvent()
        {
            HandleKeyUp?.Invoke(Key, parentKeyboard);
        }

        public float GetKeyScale()
        {
            return keyWidthScale;
        }
    }
}