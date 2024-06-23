using UnityEngine;
using AK.Wwise;

public class TriggerSwitch : AkTriggerBase
{
    [SerializeField] private GameObject _triggerObject;
    [SerializeField] private Switch _inSwitch = new();
    [SerializeField] private Switch _outSwitch = new();

    private bool _stay;
    private bool _currentSwitchState;

    private void FixedUpdate()
    {
        if (_currentSwitchState != _stay)
        {
            SetSwitch(_stay);
            _currentSwitchState = _stay;
        }

        _stay = false;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _triggerObject) 
            _stay = true;
    }
    
    private void SetSwitch(bool state)
    {
        Switch akSwitch = state ? _inSwitch : _outSwitch;
        AkSoundEngine.SetSwitch(akSwitch.GroupId, akSwitch.Id, gameObject);
    }
}
