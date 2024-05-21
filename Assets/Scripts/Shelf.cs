using UnityEngine;

using System.Collections;

public class Shelf : ClickableObject
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _openingSpeed = 1f;
    [SerializeField] private Vector3 _openState;
    [SerializeField] private Vector3 _closedState;
    [SerializeField] private bool _isOpenedByDefault;

    [SerializeField] private AkEvent _startEvent;
    [SerializeField] private AkEvent _movingPausedEvent;
    [SerializeField] private AkEvent _closedEvent;
    [SerializeField] private AkEvent _openedStateEvent;
    
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
        if (_transform == null)
            _transform = transform;
        
        _closedState = _transform.localPosition;
    }
    
    [ContextMenu("Set Open State")]
    private void SetOpenState()
    {
        _openState = _transform.localPosition;
    }
    
    [ContextMenu("Set Closed State")]
    private void SetClosedState()
    {
        _closedState = _transform.localPosition;
    }

    public override void OnClick()
    {
        _isOpening = !_isOpening;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _movingPausedEvent?.HandleEvent(null);
        }
        
        _coroutine = StartCoroutine(OpenCloseDoor());
    }

    private IEnumerator OpenCloseDoor()
    {
        _startEvent?.HandleEvent(null);
        Vector3 targetState = _isOpening ? _openState : _closedState;
        while (Vector3.Distance(_transform.localPosition, targetState) > 0.001f)
        {
            _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, targetState, _openingSpeed * Time.deltaTime);
            yield return null;
        }
        
        _coroutine = null;
        if (_isOpening)
            _openedStateEvent?.HandleEvent(null);
        else
            _closedEvent?.HandleEvent(null);
    }
}
