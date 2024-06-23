using System;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : ClickableObject
{
    [Serializable]
    public class InteractionEvent
    {
        public InteractableMaterialType MaterialType;
        public AK.Wwise.Event Event;
    }
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _minPushForce = 0f;
    [SerializeField] private float _maxPushForce = 1000f;
    [SerializeField] private float _minPushTorgue = 0f;
    [SerializeField] private float _maxPushTorgue = 1000f;
    
    [SerializeField] private List<InteractionEvent> _interactionEvents;
    [SerializeField] private float _minInteractionInterval = 0.1f;
    
    private float _lastInteractionTime;
    private InteractionEvent _defaultInteractionEvent;

    private Camera _camera;

    private void Awake()
    {
        _defaultInteractionEvent = _interactionEvents.Find(ev => ev.MaterialType == InteractableMaterialType.Default);
    }

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

    public void OnCollisionEnter(Collision other)
    {
        if (Time.time - _lastInteractionTime < _minInteractionInterval)
            return;

        InteractionEvent eventToHandle = _defaultInteractionEvent;
        
        InteractableMaterialSource materialSource = other.gameObject.GetComponent<InteractableMaterialSource>();
        if (materialSource != null)
        {
            foreach (InteractionEvent interactionEvent in _interactionEvents)
            {
                if (interactionEvent.MaterialType == materialSource.MaterialType)
                {
                    eventToHandle = interactionEvent;
                    break;
                }
            }
        }
        
        if (eventToHandle != null)
        {
            eventToHandle.Event.Post(gameObject);
            _lastInteractionTime = Time.time;
        }
    }
}