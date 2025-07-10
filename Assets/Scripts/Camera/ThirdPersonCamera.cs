using UnityEngine;

/// <summary>
/// Simple third-person camera that smoothly follows a target with
/// optional mouse orbit controls.
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    [Tooltip("Target transform the camera follows.")]
    public Transform target;

    [Tooltip("Offset from the target in local space.")]
    public Vector3 offset = new Vector3(0f, 2f, -4f);

    [Tooltip("How quickly the camera moves to the desired position.")]
    public float positionDamping = 5f;

    [Tooltip("Mouse sensitivity for orbiting around the target.")]
    public float orbitSensitivity = 2f;

    private Vector2 orbitAngles;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        HandleOrbit();

        Quaternion orbitRotation = Quaternion.Euler(orbitAngles.x, orbitAngles.y, 0f);
        Vector3 desiredPosition = target.position + orbitRotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionDamping * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, positionDamping * Time.deltaTime);
    }

    /// <summary>
    /// Adjusts the orbit angles when holding the right mouse button.
    /// </summary>
    private void HandleOrbit()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * orbitSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * orbitSensitivity;
            orbitAngles.x = Mathf.Clamp(orbitAngles.x - mouseY, -80f, 80f);
            orbitAngles.y += mouseX;
        }
    }
}
