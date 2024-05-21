using System;
using UnityEngine;
using UnityEngine.Events;

public class Lightswitch : ClickableObject
{
    [SerializeField] private GameObject _target;
    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private Behaviour _targetComponent;
    [SerializeField] private AkAmbient _sound;

    private void Awake()
    {
        if (!_sound)
            _sound = GetComponent<AkAmbient>();
    }

    private void Reset()
    {
        if (!_sound)
            _sound = GetComponent<AkAmbient>();
    }

    public override void OnClick()
    {
        if (_target)
            _target.SetActive(!_target.activeSelf);
        if (_targetComponent)
            _targetComponent.enabled = !_targetComponent.enabled;
        _unityEvent?.Invoke();
        _sound.HandleEvent(null);
    }
}
