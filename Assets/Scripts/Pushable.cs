using System;
using UnityEngine;

public class Pushable : ClickableObject
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _minPushForce = 0f;
    [SerializeField] private float _maxPushForce = 1000f;
    [SerializeField] private float _minPushTorgue = 0f;
    [SerializeField] private float _maxPushTorgue = 1000f;

    private Camera _camera;

    private void Reset()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (!Camera.main)
            throw new Exception("Main camera not found");
        _camera = Camera.main;
    }

    public override void OnClick()
    {
        // push ridigbody in random direction
        float pushForce = UnityEngine.Random.Range(_minPushForce, _maxPushForce);
        _rigidbody.AddForce(UnityEngine.Random.insideUnitSphere * pushForce);
        // add random torgue
        float pushTorgue = UnityEngine.Random.Range(_minPushTorgue, _maxPushTorgue);
        _rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * pushTorgue);
    }
}