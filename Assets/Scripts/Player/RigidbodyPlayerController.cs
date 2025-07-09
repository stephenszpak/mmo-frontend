using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Rigidbody-based player controller that handles movement using the
/// Unity Input System.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class RigidbodyPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Maximum movement speed in units per second.")]
    [SerializeField] private float moveSpeed = 5f;

    [Tooltip("Acceleration in units per second squared.")]
    [SerializeField] private float acceleration = 20f;

    [Tooltip("Deceleration in units per second squared.")]
    [SerializeField] private float deceleration = 20f;

    [Tooltip("Rotate the character to face the movement direction.")]
    [SerializeField] private bool rotateTowardMovement = true;

    [Tooltip("Optional transform used to determine movement orientation (e.g. camera).")]
    [SerializeField] private Transform orientation;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    /// <summary>
    /// Applies smooth acceleration / deceleration based on player input.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 inputDir = new Vector3(input.x, 0f, input.y);
        if (orientation != null)
        {
            inputDir = orientation.TransformDirection(inputDir);
            inputDir.y = 0f;
        }
        Vector3 desiredVelocity = inputDir * moveSpeed;

        float accelRate = input.sqrMagnitude > 0f ? acceleration : deceleration;
        Vector3 horizVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        horizVel = Vector3.MoveTowards(horizVel, desiredVelocity, accelRate * Time.fixedDeltaTime);

        rb.velocity = new Vector3(horizVel.x, rb.velocity.y, horizVel.z);

        if (rotateTowardMovement && input.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizVel.normalized);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }
}

