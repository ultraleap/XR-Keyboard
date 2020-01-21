using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockKeyboard : MonoBehaviour
{

    [SerializeField]
    private ToggleSwitchButton toggleSwitchButton;    
    
    [SerializeField]
    private KeyboardButtonManager kbManager;

    KeyboardType _previousType = KeyboardType.LOCKED;
    bool lockedSuccessfully = false;

    void Update()
    {
        if (toggleSwitchButton.On && kbManager.KeyType != KeyboardType.LOCKED && !lockedSuccessfully)
        {
            _previousType = kbManager.KeyType;
            kbManager.KeyType = KeyboardType.LOCKED;
            lockedSuccessfully = true;

        } else if (!toggleSwitchButton.On && kbManager.keyType != _previousType && lockedSuccessfully == true)
        {
            kbManager.KeyType = _previousType;
            lockedSuccessfully = false;
        }
    }
}
