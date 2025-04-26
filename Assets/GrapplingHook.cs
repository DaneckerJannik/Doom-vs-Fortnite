using UnityEngine;
using StarterAssets;

public class GrapplingHook : MonoBehaviour
{
    public Transform cameraTransform;
    public float maxGrappleDistance = 30f;
    public float grappleSpeed = 20f;
    public LayerMask grappleLayer;

    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private bool isGrappling = false;
    private Vector3 grapplePoint;

    private WallGrab _wallGrab;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        _wallGrab = GetComponent<WallGrab>();
    }

    private void Update()
    {
        if (_input.grapple && !isGrappling && !_wallGrab.IsWallGrabbing)
        {
            TryStartGrapple();
            _input.grapple = false;
        }

        if (isGrappling)
        {
            PerformGrapple();
        }
    }

    private void TryStartGrapple()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrappleDistance, grappleLayer))
        {
            grapplePoint = hit.point;
            isGrappling = true;
        }
    }

    private void PerformGrapple()
    {
        Vector3 direction = (grapplePoint - transform.position).normalized;
        _controller.Move(direction * grappleSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, grapplePoint);
        if (distance < 2f)
        {
            isGrappling = false;
        }
    }

    public void CancelGrapple()
    {
        isGrappling = false;
    }
}
