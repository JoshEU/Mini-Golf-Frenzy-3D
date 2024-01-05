using UnityEngine;

// This Class teleports the player when they come into contact with the tunnel teleporter trigger used within Level 2.
public class TeleportPlayer : MonoBehaviour {
    // External Script variables:
    [SerializeField]
    private CameraMovement cameraMovementScript;
    [SerializeField]
    private GameObject teleportDestinationObj;

    // If Golf ball enters teleport trigger, teleport them to the destination's position (set inside the inspector).
	private void OnTriggerEnter(Collider teleporter) {
        if (teleporter.gameObject.name == "TeleportIn") {
            cameraMovementScript.golfBallObj.transform.position = teleportDestinationObj.transform.position;
        }
	}
}