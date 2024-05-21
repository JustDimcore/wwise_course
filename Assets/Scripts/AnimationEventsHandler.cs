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
    
    private void JumpStart()
    {
        Debug.Log("JumpStart anim event");
        _jumpStartEvent?.HandleEvent(null);
    }
    
    private void JumpFloorTouch()
    {
        Debug.Log("JumpFloorTouch anim event");
        _jumpFloorTouchEvent?.HandleEvent(null);
    }
    
    private void JumpFloorTouchLow()
    {
        Debug.Log("JumpFloorTouchLow anim event");
        _jumpFloorTouchLowEvent?.HandleEvent(null);
    }
    
    private void JumpMoveStart()
    {
        Debug.Log("JumpMoveStart anim event");
        _jumpMoveStartEvent?.HandleEvent(null);
    }
    
    private void StepWalk()
    {
        Debug.Log("StepWalk anim event");
        _stepWalkEvent?.HandleEvent(null);
    }
    
    private void StepWalkRun()
    {
        Debug.Log("StepWalkRun anim event");
        _stepWalkRunEvent?.HandleEvent(null);
    }
    
    private void StepRun()
    {
        Debug.Log("StepRun anim event");
        _stepRunEvent?.HandleEvent(null);
    }
}