using UnityEngine;
using UnityEngine.Events;

public class ClickableLauncher : ClickableObject
{
    [SerializeField] private UnityEvent _unityEvent;
    
    public override void OnClick()
    {
        _unityEvent?.Invoke();
    }
}
