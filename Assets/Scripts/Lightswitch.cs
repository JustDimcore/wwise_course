using System;
using UnityEngine;

public class LightSwitch : ClickableObject
{
    [SerializeField] private Behaviour _targetComponent;
    [SerializeField] private AK.Wwise.Event lightSwitch; // Івент для перемикання стану світла

    private void Awake()
    {
        // Перевірка, чи встановлений івент через інспектор
        if (lightSwitch == null)
        {
            Debug.LogWarning("Івент перемикання світла не встановлений в інспекторі.");
        }
    }

    public override void OnClick()
    {
        if (_targetComponent)
            _targetComponent.enabled = !_targetComponent.enabled;

        lightSwitch.Post(gameObject); // Виклик івенту перемикання стану світла
    }
}