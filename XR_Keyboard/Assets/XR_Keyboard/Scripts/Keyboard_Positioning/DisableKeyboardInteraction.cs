using UnityEngine;

namespace Leap.Unity.Interaction.Keyboard
{
    public class DisableKeyboardInteraction : MonoBehaviour
    {
        public void DisableKeys()
        {
            InteractionButton[] keys = transform.GetComponentsInChildren<InteractionButton>();
            foreach (var key in keys)
            {
                key.controlEnabled = false;
            }
        }

        public void EnableKeys()
        {
            InteractionButton[] keys = transform.GetComponentsInChildren<InteractionButton>();
            foreach (var key in keys)
            {
                key.controlEnabled = true;
            }
        }
    }
}