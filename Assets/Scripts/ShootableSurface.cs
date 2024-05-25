using UnityEngine;

public class ShootableSurface : Shootable
{
    [SerializeField] private AkEvent _shotSound;
    [SerializeField] private ParticleSystem _shotVfx;
    
    public override void OnShot(RaycastHit hitInfo, Vector3 transformPosition)
    {
        _shotSound.HandleEvent(null);
        Transform shotVfxTransform = _shotVfx.transform;
        shotVfxTransform.position = hitInfo.point;
        shotVfxTransform.forward = hitInfo.normal;
    }
}