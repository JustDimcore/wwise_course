using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInput : MonoBehaviour
{
    // #region Variables       
    //
    // [Header("Controller Input")]
    // public string horizontalInput = "Horizontal";
    // public string verticallInput = "Vertical";
    // public KeyCode jumpInput = KeyCode.Space;
    // public KeyCode strafeInput = KeyCode.Tab;
    // public KeyCode sprintInput = KeyCode.LeftShift;
    //
    // [Header("Camera Input")]
    // public string rotateCameraXInput = "Mouse X";
    // public string rotateCameraYInput = "Mouse Y";
    //
    // // [HideInInspector] public vThirdPersonController cc;
    // [SerializeField] private PlayerController _player;
    // [SerializeField] private vThirdPersonCamera _tpCamera;
    // [SerializeField] private Camera _cameraMain;
    //
    // #endregion
    //
    // protected virtual void Start()
    // {
    //     InitilizeController();
    //     InitializeTpCamera();
    // }
    //
    // protected virtual void FixedUpdate()
    // {
    //     cc.UpdateMotor();               // updates the ThirdPersonMotor methods
    //     cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
    //     cc.ControlRotationType();       // handle the controller rotation type
    // }
    //
    // protected virtual void Update()
    // {
    //     InputHandle();                  // update the input methods
    //     // cc.UpdateAnimator();            // updates the Animator Parameters
    // }
    //
    // public virtual void OnAnimatorMove()
    // {
    //     cc.ControlAnimatorRootMotion(); // handle root motion animations 
    // }
    //
    // // protected virtual void InitilizeController()
    // // {
    // //     cc = GetComponent<vThirdPersonController>();
    // //
    // //     if (cc != null)
    // //         cc.Init();
    // // }
    //
    // protected virtual void InitializeTpCamera()
    // {
    //     // if (_tpCamera == null)
    //     // {
    //     //     _tpCamera = FindObjectOfType<vThirdPersonCamera>();
    //     //     if (_tpCamera == null)
    //     //         return;
    //     //     if (_tpCamera)
    //     //     {
    //             _tpCamera.SetMainTarget(this.transform);
    //             _tpCamera.Init();
    //         // }
    //     // }
    // }
    //
    // protected virtual void InputHandle()
    // {
    //     MoveInput();
    //     CameraInput();
    //     SprintInput();
    //     StrafeInput();
    //     JumpInput();
    // }
    //
    // public virtual void MoveInput()
    // {
    //     cc.input.x = Input.GetAxis(horizontalInput);
    //     cc.input.z = Input.GetAxis(verticallInput);
    // }
    //
    // protected virtual void CameraInput()
    // {
    //     if (!_cameraMain)
    //     {
    //         if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
    //         else
    //         {
    //             _cameraMain = Camera.main;
    //             cc.rotateTarget = _cameraMain.transform;
    //         }
    //     }
    //
    //     if (_cameraMain)
    //     {
    //         cc.UpdateMoveDirection(_cameraMain.transform);
    //     }
    //
    //     if (_tpCamera == null)
    //         return;
    //
    //     var Y = Input.GetAxis(rotateCameraYInput);
    //     var X = Input.GetAxis(rotateCameraXInput);
    //
    //     _tpCamera.RotateCamera(X, Y);
    // }
    //
    // protected virtual void StrafeInput()
    // {
    //     if (Input.GetKeyDown(strafeInput))
    //         cc.Strafe();
    // }
    //
    // // protected virtual void SprintInput()
    // // {
    // //     if (Input.GetKeyDown(sprintInput))
    // //         cc.Sprint(true);
    // //     else if (Input.GetKeyUp(sprintInput))
    // //         cc.Sprint(false);
    // // }
    //
    // /// <summary>
    // /// Conditions to trigger the Jump animation & behavior
    // /// </summary>
    // /// <returns></returns>
    // protected virtual bool JumpConditions()
    // {
    //     return _player.IsGrounded && _player.GroundAngle() < _player.slopeLimit && !_player.isJumping && !_player.stopMove;
    // }
    //
    // /// <summary>
    // /// Input to trigger the Jump 
    // /// </summary>
    // protected virtual void JumpInput()
    // {
    //     if (Input.GetKeyDown(jumpInput) && JumpConditions())
    //         cc.Jump();
    // }
}