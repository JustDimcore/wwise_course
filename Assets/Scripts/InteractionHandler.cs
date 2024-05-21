using Invector.vCharacterController;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    public enum InteractionMode
    {
        None = 0,
        Interactive = 1,
        Attack = 2,
    }

    [SerializeField] private Camera _camera;
    [SerializeField] private vThirdPersonController _controller;
    [SerializeField] private vThirdPersonInput _input;

    // [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private Image _cursorImage;

    [SerializeField] private Sprite _defaultCursor;
    [SerializeField] private Sprite _interactiveCursor;
    [SerializeField] private Sprite _attackCursor;

    private InteractionMode _currentMode = InteractionMode.None;
    private ClickableObject _clickableTarget;

    private void Awake()
    {
        if (!_camera)
            _camera = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        SetCursor(_defaultCursor);
    }

    private void SetCursor(Sprite cursor)
    {
        _cursorImage.sprite = cursor;
    }

    private void Update()
    {
        CheckInteractivity();
        CheckClick();
    }

    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentMode == InteractionMode.Interactive)
            {
                _clickableTarget.OnClick();
            }
        }
    }

    private void CheckInteractivity()
    {
        var isInteractive = CanInteract(out _clickableTarget);
        var newMode = isInteractive ? InteractionMode.Interactive : InteractionMode.None;
        if (newMode != _currentMode)
        {
            _currentMode = newMode;
            OnModeChanged(_currentMode);
        }

        _input.IsShootingLocked = newMode == InteractionMode.Interactive;
    }

    private void OnModeChanged(InteractionMode mode)
    {
        Sprite cursor = mode switch
        {
            InteractionMode.Interactive => _interactiveCursor,
            _ => _defaultCursor
        };
        SetCursor(cursor);
    }

    private bool CanInteract(out ClickableObject clickableObject)
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0); // Input.mousePosition

        // Cast a ray from the main camera to the center of the screen
        Ray ray = _camera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out var hit, 10, _interactionLayer))
        {
            Transform hitTransform = hit.transform;
            if (hitTransform)
            {
                clickableObject = hitTransform.GetComponent<ClickableObject>();
                return clickableObject && clickableObject.enabled;
            }
        }

        clickableObject = null;
        return false;
    }
}