using System;
using Invector.vCharacterController;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] private AkEvent _jumpStartEvent;
    [SerializeField] private AkEvent _jumpFloorTouchEvent;
    [SerializeField] private AkEvent _jumpFloorTouchLowEvent;
    [SerializeField] private AkEvent _jumpMoveStartEvent;
    [SerializeField] private AkEvent _stepWalkEvent;
    [SerializeField] private AkEvent _stepWalkRunEvent;
    [SerializeField] private AkEvent _stepRunEvent;

    [SerializeField] private AkSwitch _runSwitch;
    [SerializeField] private AkSwitch _walkSwitch;
    [SerializeField] private vThirdPersonAnimator _animator;

    private bool _isLastFrameRunning;

    private void Update()
    {
        if (_animator.isSprinting)
        {
            if (!_isLastFrameRunning)
            {
                _runSwitch.HandleEvent(gameObject);
                _isLastFrameRunning = true;
                Debug.Log("Switched to run");
            }
            _isLastFrameRunning = true;
        }
        else
        {
            if (_isLastFrameRunning)
            {
                _walkSwitch.HandleEvent(gameObject);
                _isLastFrameRunning = false;
                Debug.Log("Switched to walk");
            }
            _isLastFrameRunning = false;
        }
    }

    private void JumpStart()
    {
        Debug.Log("JumpStart anim event");
        _jumpStartEvent?.HandleEvent(gameObject);
    }
    
    private void JumpFloorTouch()
    {
        Debug.Log("JumpFloorTouch anim event");
        _jumpFloorTouchEvent?.HandleEvent(gameObject);
    }
    
    private void JumpFloorTouchLow()
    {
        Debug.Log("JumpFloorTouchLow anim event");
        _jumpFloorTouchLowEvent?.HandleEvent(gameObject);
    }
    
    private void JumpMoveStart()
    {
        Debug.Log("JumpMoveStart anim event");
        _jumpMoveStartEvent?.HandleEvent(gameObject);
    }
    
    private void StepWalk()
    {
        if (!_animator.stopMove && _animator.inputMagnitude is > 0.001f and <= 1f)
        {
            _stepWalkEvent?.HandleEvent(gameObject);
            Debug.Log("Walk event " + _animator.inputMagnitude);
        }
    }
    
    private void StepWalkRun()
    {
        // _stepWalkRunEvent?.HandleEvent(null);
        // Debug.Log("WalkRun event");
    }
    
    private void StepRun()
    {
        if (!_animator.stopMove && _animator.inputMagnitude > 1f)
        {
            _stepRunEvent?.HandleEvent(gameObject);
            Debug.Log("Run event " + _animator.inputMagnitude);
        }
    }
}