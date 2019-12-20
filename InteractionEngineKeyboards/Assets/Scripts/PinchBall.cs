using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Interaction;

public class PinchBall : MonoBehaviour
{
    [SerializeField] Leap.Unity.Chirality _chirality;

    bool isLeft { get { return _chirality == Chirality.Left ? true : false; } }

    LeapProvider _leapProvider;

    [SerializeField] Bone.BoneType _midPointBones = Bone.BoneType.TYPE_INVALID;

    [SerializeField] [Range(0.01f, 0.1f)] float _distanceThreshold = 0.01f;
    float _currentDistance = -1f;
    bool _oldPress = false, _currentPress = false;
    PinchInputButton _currentButton = null;
    PinchInputButton _pressedButton;

    Collider _collider;

    ContactBone _tempBone;

    private void OnEnable()
    {
        _leapProvider = FindObjectOfType<LeapProvider>();
        _leapProvider.OnUpdateFrame += OnUpdateFrame;
        _collider = GetComponent<Collider>();
    }


    private void OnDisable()
    {
        _leapProvider.OnUpdateFrame -= OnUpdateFrame;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out _tempBone))
        {
            Physics.IgnoreCollision(_tempBone.collider, _collider);
        }
        if(other.gameObject.GetComponent<PinchInputButton>() != null)
        {
            // i know this isn't exactly great
            _currentButton = other.gameObject.GetComponent<PinchInputButton>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(_currentButton == null && other.gameObject.GetComponent<PinchInputButton>() != null)
        {
            _currentButton = other.gameObject.GetComponent<PinchInputButton>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PinchInputButton>() != null)
        {
            _currentButton = null;
            if(_pressedButton != null)
            {
                _pressedButton.Unpressed();
                _pressedButton = null;
            }
        }
    }

    private void OnUpdateFrame(Leap.Frame obj)
    {
        if (obj.Hands.Count == 0)
        {
            _currentDistance = -1f;
            _currentPress = false;
            CheckPressEvent();
            return;
        }

        int handIndex = 0;
        if (obj.Hands.Count == 1 && obj.Hands[0].IsLeft != isLeft)
        {
            _currentDistance = -1f;
            _currentPress = false;
            CheckPressEvent();
            return;
        }

        if (obj.Hands.Count == 2)
        {
            handIndex = obj.Hands[0].IsLeft == isLeft ? 0 : 1;
        }
        Hand currentHand = obj.Hands[handIndex];

        Vector3 thumbPos = currentHand.Fingers[0].Bone(_midPointBones).Center.ToVector3();
        Vector3 indexPos = currentHand.Fingers[1].Bone(_midPointBones).Center.ToVector3();
        Vector3 thumbTipPos = currentHand.Fingers[0].Bone(Bone.BoneType.TYPE_DISTAL).Center.ToVector3();
        Vector3 indexTipPos = currentHand.Fingers[1].Bone(Bone.BoneType.TYPE_DISTAL).Center.ToVector3();

        transform.LookAt(Vector3.Lerp(thumbTipPos, indexTipPos, 0.5f));
        transform.position = Vector3.Lerp(thumbPos, indexPos, 0.5f);
        _currentDistance = Vector3.Distance(thumbTipPos, indexTipPos);

        if (_currentDistance <= _distanceThreshold)
        {
            //Debug.Log("in threshold!");
            _currentPress = true;
        }
        else
        {
            _currentPress = false;
        }
        CheckPressEvent();

    }

    void CheckPressEvent()
    {
        if(_oldPress != _currentPress)
        {
            if(_currentPress)
            {
                if(_currentButton != null)
                {
                    _pressedButton = _currentButton;
                    _pressedButton.Pressed();
                }
            }
            else
            {
                if (_pressedButton != null)
                {
                    _pressedButton.Unpressed();
                    _pressedButton = null;
                }
            }
        }
        _oldPress = _currentPress;
    }
}
