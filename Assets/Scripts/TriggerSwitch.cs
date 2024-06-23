using UnityEngine;
using AK.Wwise;

public class TriggerSwitch : AkTriggerBase
{
    [SerializeField] private GameObject _triggerObject;
    [SerializeField] private Switch _inSwitch = new();
    [SerializeField] private Switch _outSwitch = new();

    private int _enterCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _triggerObject)
            if (++_enterCounter == 1)
                SetSwitch(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _triggerObject)
            if (--_enterCounter == 0)
                SetSwitch(false);
    }

    private void SetSwitch(bool state) => (state ? _inSwitch : _outSwitch).SetValue(gameObject);
}
