using UnityEngine;

/// <summary>
/// Simulates UDP send and receive logs for development.
/// </summary>
public class DummyNetworkManager : MonoBehaviour
{
    /// <summary>
    /// Logs that the dummy network manager is active.
    /// </summary>
    private void Start()
    {
        Debug.Log("[Network] Dummy network manager initialized.");
    }

    /// <summary>
    /// Logs simulated packet transmission when the player moves.
    /// </summary>
    /// <param name="position">Current player position.</param>
    public void SendPlayerPosition(Vector3 position)
    {
        Debug.Log($"[Network] UDP Send: {position}");
        Debug.Log($"[Network] UDP Receive: ack for {position}");
    }
}

