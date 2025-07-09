using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

/// <summary>
/// Handles player movement, camera rotation and jump logic.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Movement speed in units per second.")]
    public float moveSpeed = 5f;

    [Tooltip("Jump height in meters.")]
    public float jumpHeight = 2f;

    [Tooltip("Gravity force.")]
    public float gravity = -9.81f;

    [Header("Camera")]
    [Tooltip("Root transform for the camera to rotate around.")]
    public Transform cameraRoot;

    [Tooltip("Cinemachine virtual camera following the player.")]
    public CinemachineVirtualCamera followCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 lastSentPosition;
    private DummyNetworkManager network;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private const float lookSensitivity = 0.1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        network = GetComponent<DummyNetworkManager>();
        
        var map = new InputActionMap("Player");
        moveAction = map.AddAction("Move", binding: "<Gamepad>/leftStick");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        lookAction = map.AddAction("Look", binding: "<Mouse>/delta");
        jumpAction = map.AddAction("Jump", binding: "<Keyboard>/space");
        map.Enable();

        if (followCamera != null)
        {
            followCamera.Follow = cameraRoot;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJump();
    }

    /// <summary>
    /// Reads WASD movement and moves the CharacterController.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        direction = Quaternion.Euler(0f, cameraRoot.eulerAngles.y, 0f) * direction;
        controller.Move(direction * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // Small downward force to stick to ground
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (network != null && Vector3.Distance(transform.position, lastSentPosition) > 0.1f)
        {
            network.SendPlayerPosition(transform.position);
            lastSentPosition = transform.position;
        }
    }

    /// <summary>
    /// Handles mouse-controlled camera rotation while holding the right button.
    /// </summary>
    private void HandleMouseLook()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 lookDelta = lookAction.ReadValue<Vector2>();
            cameraRoot.Rotate(Vector3.up, lookDelta.x * lookSensitivity, Space.World);
        }
    }

    /// <summary>
    /// Applies jump velocity when the jump action is triggered.
    /// </summary>
    private void HandleJump()
    {
        if (jumpAction.triggered && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}

