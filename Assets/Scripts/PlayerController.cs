using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] private PlayerAnimationController _playerAnimationController;
    // [SerializeField] private CapsuleCollider _capsuleCollider;                                          // access CapsuleCollider information
    //
    // [Tooltip("Layers that the character can walk on")]
    // [SerializeField] private LayerMask _groundLayer = 1 << 0;
    // [Tooltip("Distance to became not grounded")]
    // [SerializeField] private float _groundMinDistance = 0.25f;
    // [SerializeField] private float _groundMaxDistance = 0.5f;
    //
    // private RaycastHit _groundHit; 
    // private Vector3 _colliderCenter;
    // private float _colliderRadius;
    // private float _colliderHeight;
    // private float _groundDistance;
    //
    // public bool IsGrounded { get; set; } = true;
    //
    // private void Awake()
    // {
    //     // save your collider preferences 
    //     _colliderCenter = _capsuleCollider.center;
    //     _colliderRadius = _capsuleCollider.radius;
    //     _colliderHeight = _capsuleCollider.height;
    // }
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
    //
    // public virtual float GroundAngle()
    // {
    //     var groundAngle = Vector3.Angle(_groundHit.normal, Vector3.up);
    //     return groundAngle;
    // }
    //
    // protected virtual void CheckGroundDistance()
    // {
    //     if (_capsuleCollider != null)
    //     {
    //         // radius of the SphereCast
    //         float radius = _capsuleCollider.radius * 0.9f;
    //         float dist = 10f;
    //         // ray for RayCast
    //         Ray ray2 = new Ray(transform.position + new Vector3(0, _colliderHeight / 2, 0), Vector3.down);
    //         // raycast for check the ground distance
    //         if (Physics.Raycast(ray2, out _groundHit, (_colliderHeight / 2) + dist, _groundLayer) && !_groundHit.collider.isTrigger)
    //             dist = transform.position.y - _groundHit.point.y;
    //         // sphere cast around the base of the capsule to check the ground distance
    //         if (dist >= _groundMinDistance)
    //         {
    //             Vector3 pos = transform.position + Vector3.up * (_capsuleCollider.radius);
    //             Ray ray = new Ray(pos, -Vector3.up);
    //             if (Physics.SphereCast(ray, radius, out _groundHit, _capsuleCollider.radius + _groundMaxDistance, _groundLayer) && !_groundHit.collider.isTrigger)
    //             {
    //                 Physics.Linecast(_groundHit.point + (Vector3.up * 0.1f), _groundHit.point + Vector3.down * 0.15f, out _groundHit, _groundLayer);
    //                 float newDist = transform.position.y - _groundHit.point.y;
    //                 if (dist > newDist) dist = newDist;
    //             }
    //         }
    //         _groundDistance = (float)Math.Round(dist, 2);
    //     }
    // }
    //
    // public virtual void UpdateMotor()
    // {
    //     CheckGround();
    //     CheckSlopeLimit();
    //     ControlJumpBehaviour();
    //     AirControl();
    // }
    //
    // protected virtual void CheckGround()
    // {
    //     CheckGroundDistance();
    //     ControlMaterialPhysics();
    //
    //     if (_groundDistance <= _groundMinDistance)
    //     {
    //         IsGrounded = true;
    //         if (!isJumping && _groundDistance > 0.05f)
    //             _rigidbody.AddForce(transform.up * (extraGravity * 2 * Time.deltaTime), ForceMode.VelocityChange);
    //
    //         heightReached = transform.position.y;
    //     }
    //     else
    //     {
    //         if (_groundDistance >= _groundMaxDistance)
    //         {
    //             // set IsGrounded to false 
    //             IsGrounded = false;
    //             // check vertical velocity
    //             verticalVelocity = _rigidbody.velocity.y;
    //             // apply extra gravity when falling
    //             if (!isJumping)
    //             {
    //                 _rigidbody.AddForce(transform.up * extraGravity * Time.deltaTime, ForceMode.VelocityChange);
    //             }
    //         }
    //         else if (!isJumping)
    //         {
    //             _rigidbody.AddForce(transform.up * (extraGravity * 2 * Time.deltaTime), ForceMode.VelocityChange);
    //         }
    //     }
    // }
}
