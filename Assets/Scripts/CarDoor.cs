using System;
using UnityEngine;
using System.Collections;

public class CarDoor : ClickableObject
{
    [SerializeField] private Transform _door;
    [SerializeField] private float _openingSpeed = 90f;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _closedAngle = 0f;
    [SerializeField] private bool _isOpenedByDefault;

    [SerializeField] private AK.Wwise.Event carDoorOpened; // Івент для відкриття
    [SerializeField] private AK.Wwise.Event carDoorClosed; // Івент для закриття
    [SerializeField] private AK.Wwise.Event carDoorMovingStart; // Івент для початку руху дверей

    private bool _isOpening;
    private Coroutine _coroutine;

    private void Awake()
    {
        _isOpening = _isOpenedByDefault;
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
        }

        if (_isOpening && Mathf.Approximately(_door.localEulerAngles.y, _closedAngle))
        {
            carDoorOpened?.Post(gameObject); // Виклик івенту відкриття при кліці, якщо двері повністю закриті (Rotation Y дорівнює 0)
        }

        carDoorMovingStart?.Post(gameObject); // Виклик івенту руху дверей при кожному кліці
        _coroutine = StartCoroutine(OpenCloseDoor());
    }

    private IEnumerator OpenCloseDoor()
    {
        float targetAngle = _isOpening ? _openAngle : _closedAngle;
        while (Mathf.Abs(Mathf.DeltaAngle(_door.localEulerAngles.y, targetAngle)) > 0.1f)
        {
            _door.localEulerAngles = new Vector3(
                _door.localEulerAngles.x,
                Mathf.MoveTowardsAngle(_door.localEulerAngles.y, targetAngle, _openingSpeed * Time.deltaTime),
                _door.localEulerAngles.z
            );
            yield return null;
        }
        _coroutine = null;

        if (!_isOpening)
            carDoorClosed?.Post(gameObject); // Виклик івенту закриття після завершення руху дверей
    }
}