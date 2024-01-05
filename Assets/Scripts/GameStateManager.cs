using UnityEngine;

// This Class contains the states at which the golf ball can be in during the game.
public class GameStateManager : MonoBehaviour {
    // External Scripts variables:
    [SerializeField]
    private CameraMovement cameraMovementScript;
    [SerializeField]
    private GolfBallController golfBallControllerScript;
    [SerializeField]
    private GUIManager guiManagerScript;
    [SerializeField]
    private Win winScript;
    // Game State bools:
    public static bool isIdle = true;
    public static bool isAiming = true;
    public static bool isShooting = false;
    public static bool isDead = false;
    public static bool inHole = false;

    // Awake is called before Start
    private void Awake() {
        // Initialises Game State bools to default values
        isIdle = true;
        isAiming = true;
        isShooting = false;
        isDead = false;
        inHole = false;
    }
    private void Update() {
        CurrentGameState();
    }
    // Checks which game state we are currently in.
    // Separate if statements as some states may be true simultaneously (at the same time).
    private void CurrentGameState() {
        // Re-enables the Power Shot slider and checks for the golf ball's current position in case it enters Death state and needs to respawn.
        if (isIdle) {
            isAiming = true;
            isShooting = false;
            golfBallControllerScript.CheckCurrentPosition();
            guiManagerScript.EnableShotPowerSlider();
        }
        // Determines the aiming direction by using the Camera's current forward facing direction.
        if (isAiming) {
            golfBallControllerScript.CheckAimingDirection();
        }
        // Disables shot power slider so the player cannot continue to shoot the golf ball whilst its in motion.
        if (isShooting) {
            guiManagerScript.DisableShotPowerSlider();
        }
        // Respawns the golf ball to its most recent position in Idle state.
        if (isDead) {
            golfBallControllerScript.RespawnGolfBall();
        }
        // Plays the win function inside the Win.cs script.
        if (inHole) {
            StartCoroutine(winScript.InHole());
        }
    }
}