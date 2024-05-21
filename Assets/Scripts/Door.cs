using System;
using UnityEngine;

using System.Collections;

public class Door : ClickableObject
{
    [SerializeField] private Transform _door;
    [SerializeField] private float _openingSpeed = 90f;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _closeAngle = 0f;
    [SerializeField] private bool _isOpenedByDefault;

    [SerializeField] private AkEvent _doorMovingStart;
    [SerializeField] private AkEvent _doorMovingPaused;
    [SerializeField] private AkEvent _doorClosed;
    [SerializeField] private AkEvent _doorOpened;
    
    private bool _isOpening;
    private Coroutine _coroutine;

    private void Awake()
    {
        _isOpening = _isOpenedByDefault;
    }

    // to expose enabled/disabled state in the inspector
    private void OnEnable()
    {
    }

    private void Reset()
    {
        if (_door == null)
            _door = transform;
    }

    public override void OnClick()
    {
        _isOpening = !_isOpening;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _doorMovingPaused?.HandleEvent(null);
        }
        
        _coroutine = StartCoroutine(OpenCloseDoor());
    }

    private IEnumerator OpenCloseDoor()
    {
        _doorMovingStart?.HandleEvent(null);
        float targetAngle = _isOpening ? _openAngle : _closeAngle;
        while (Mathf.Abs(_door.localEulerAngles.y - targetAngle) > 0.1f)
        {
            _door.localEulerAngles = new Vector3(0, Mathf.MoveTowardsAngle(_door.localEulerAngles.y, targetAngle, _openingSpeed * Time.deltaTime), 0);
            yield return null;
        }
        _coroutine = null;
        if (_isOpening)
            _doorOpened?.HandleEvent(null);
        else
            _doorClosed?.HandleEvent(null);
    }
}
