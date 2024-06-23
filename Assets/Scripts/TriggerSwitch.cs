using System;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class TriggerSwitch : AkTriggerBase
{
    [SerializeField] private List<GameObject> _switchTargetObjects;
    [SerializeField] private GameObject _triggerObject;
    [SerializeField] private Switch _inSwitch = new();
    [SerializeField] private Switch _outSwitch = new();
    [SerializeField] private bool _forceDefaultState;
    [SerializeField] private bool _defaultState;

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

    private void Start()
    {
        if (_forceDefaultState)
            SetSwitch(_defaultState);
    }

    private void SetSwitch(bool state)
    {
        Switch akSwitch = state ? _inSwitch : _outSwitch;
        foreach (GameObject go in _switchTargetObjects)
            akSwitch.SetValue(go);
    }
}
