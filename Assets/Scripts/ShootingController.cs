using System.Collections;
using Invector.vCharacterController;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private vThirdPersonController _controller;
    [SerializeField] private float _preparingTime = 0.5f;
    [SerializeField] private float _shotRate = 1f;
    [SerializeField] private GameObject _muzzleVfx;
    [SerializeField] private AkAmbient _shotSound;
    
    private float _lastShotTime;
    private float _preparingTimeLeft;
    private Coroutine _coroutine;

    private void Awake()
    {
        _preparingTimeLeft = _preparingTime;
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
                    if (_coroutine != null)
                        StopCoroutine(_coroutine);
                    _coroutine = StartCoroutine(ShootCoroutine());
                }
            }
        }
        else
        {
            _preparingTimeLeft += Time.deltaTime;
            if (_preparingTimeLeft > _preparingTime)
                _preparingTimeLeft = _preparingTime;
        }
    }

    private IEnumerator ShootCoroutine()
    {
        _muzzleVfx.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _shotSound.HandleEvent(null);
        _muzzleVfx.SetActive(false);
        _coroutine = null;
    }
}