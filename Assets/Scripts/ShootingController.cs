using System.Collections;
using Invector.vCharacterController;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private vThirdPersonController _controller;
    [SerializeField] private float _preparingTime = 0.5f;
    [SerializeField] private float _shotRate = 1f;
    [SerializeField] private GameObject _muzzleVfx;
    [SerializeField] private AkAmbient _shotStartEvent;
    [SerializeField] private AkAmbient _shotStopEvent;
    [SerializeField] private Camera _camera;
    
    private float _lastShotTime;
    private float _preparingTimeLeft;
    private Coroutine _vfxCoroutine;
    private bool _isShootingSoundInProgress;
    private Coroutine _soundCoroutine;

    private void Awake()
    {
        _preparingTimeLeft = _preparingTime;
        if (!_camera)
            _camera = Camera.main;
    }

    private void Update()
    {
        if (_controller.isShooting)
        {
            if (Time.time - _lastShotTime > _shotRate)// - _shotOffset)
            {
                _preparingTimeLeft -= Time.deltaTime;
                if (_preparingTimeLeft <= 0)
                {
                    _lastShotTime = Time.time;
                    Shot();
                }
            }
        }
        else
        {
            _preparingTimeLeft += Time.deltaTime;
            if (_preparingTimeLeft > _preparingTime)
                _preparingTimeLeft = _preparingTime;

            if (_isShootingSoundInProgress)
            {
                _shotStopEvent.HandleEvent(null);
                _isShootingSoundInProgress = false;
            }
        }
    }

    private void Shot()
    {
        if (_vfxCoroutine != null)
            StopCoroutine(_vfxCoroutine);
        _vfxCoroutine = StartCoroutine(VfxCoroutine());

        if (!_isShootingSoundInProgress)
            _shotStartEvent.HandleEvent(null);
        _isShootingSoundInProgress = true;
        
        // find shootable object in the center of the screen
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Shootable shootable))
            {
                shootable.OnShot(hit, transform.position);
            }
        }
    }

    private IEnumerator VfxCoroutine()
    {
        _muzzleVfx.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _muzzleVfx.SetActive(false);
        
        _vfxCoroutine = null;
    }
}