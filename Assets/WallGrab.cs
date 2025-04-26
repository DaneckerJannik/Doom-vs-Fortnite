using UnityEngine;
using StarterAssets;

public class WallGrab : MonoBehaviour
{
    public LayerMask wallLayer;
    public float wallCheckDistance = 0.6f;
    public float wallStickTime = 1f;
    public float wallJumpForce = 8f;
    public Vector3 wallJumpDirection = new Vector3(1, 1, 0);

    private StarterAssetsInputs _input;
    private CharacterController _controller;
    private FirstPersonController _fpc;

    private bool isTouchingWall = false;
    private bool isWallGrabbing = false;
    private float wallGrabTimer;

    public bool IsWallGrabbing => isWallGrabbing;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _controller = GetComponent<CharacterController>();
        _fpc = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        CheckWall();

        if (isTouchingWall && !_fpc.Grounded && _fpc._verticalVelocity < 0)
        {
            StartWallGrab();
        }

        if (isWallGrabbing)
        {
            wallGrabTimer -= Time.deltaTime;
            _fpc._verticalVelocity = 0f;

            if (_input.jump)
            {
                DoWallJump();
            }

            if (wallGrabTimer <= 0)
            {
                StopWallGrab();
            }
        }
    }

    private void CheckWall()
    {
        Vector3 direction = transform.forward;
        isTouchingWall = Physics.Raycast(transform.position, direction, wallCheckDistance, wallLayer);
    }

    private void StartWallGrab()
    {
        // Grappling abbrechen, falls aktiv
        GrapplingHook gh = GetComponent<GrapplingHook>();
        if (gh != null)
        {
            gh.CancelGrapple();
        }

        isWallGrabbing = true;
        wallGrabTimer = wallStickTime;
    }

    private void StopWallGrab()
    {
        isWallGrabbing = false;
    }

    private void DoWallJump()
    {
        StopWallGrab();

        Vector3 jumpDir = (-transform.forward * wallJumpDirection.z + Vector3.up * wallJumpDirection.y + transform.right * wallJumpDirection.x).normalized;
        _fpc._verticalVelocity = Mathf.Sqrt(wallJumpForce * -2f * _fpc.Gravity);

        Vector3 push = jumpDir * wallJumpForce * 0.5f;
        _controller.Move(push * Time.deltaTime);

        _input.jump = false;
    }
}
