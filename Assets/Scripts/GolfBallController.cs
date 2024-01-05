using System.Collections;
using UnityEngine;

// This Class applies the functionality for the golf ball such as its aiming and movement.
// Referenced and used in the GameStateManager.cs script
public class GolfBallController : MonoBehaviour {
    // External Scripts variables:
    [SerializeField]
    private CameraMovement cameraMovementScript;
    [SerializeField]
    private GUIManager guiManagerScript;
    [SerializeField]
    private AudioManager audioManagerScript;
    // Public variables:
    // Accessed inside the Camera Movement script and altered by the cameras movement.
    // Used to determine the direction of where the golf ball is going to be hit.
    public static Vector3 golfBallDirection;
    // Default speed value for golf ball - this is altered by the shot power slider.
    public static float shotPower = 1f;
    // Private variables:
    [SerializeField]
    private GameObject cameraObj;
    private Rigidbody rb;
    // Golf balls current position - used to respawn the golf ball in its most previous position if it is in the Death State.
    private Vector3 currentSpawnPosition;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update() {
        CheckGolfBallMovement();
    }
    // Checks if the golf ball is moving and if not then isIdle is set back to true.
    // Called in Update()
    private void CheckGolfBallMovement() {
        if (rb.IsSleeping()){ 
           GameStateManager.isIdle = true;
        } 
    }
    // GAME STATE FUNCTIONS:
    // Assigns the Golf Ball's current position in world space to a Vector3 variable.
    // Called when isIdle = true in GameStateManager.cs script.
    public void CheckCurrentPosition() {
        currentSpawnPosition = cameraMovementScript.golfBallObj.transform.position;
    }
    // Checks the direction that the camera/golf ball is facing and transforms it to a new Vector3 position.
    // The golf balls aiming direction is determined by the cameras forward direction.
    // Called when isAiming = true GameStateManager.cs script.
    public void CheckAimingDirection() {
        // Checks the camera's forward direction so the aiming of the golf ball is relative to it.
        golfBallDirection = new Vector3(cameraObj.transform.forward.x, 0, cameraObj.transform.forward.z);
    }
    // This Coroutine delays the golf ball's respawn time and stops it moving by using the Sleep and WakeUp functions.
    // Called in RespawnGolfBall().
    private IEnumerator RespawnDelay() {
        rb.Sleep();
        yield return new WaitForSeconds(1.5f);
        rb.WakeUp();
    }
    // Spawns Golf Ball in its most recent position from when it was in Idle state.
    // Called when isDeath = true in GameStateManager.cs script.
    public void RespawnGolfBall() {
        StartCoroutine(RespawnDelay());
        cameraMovementScript.golfBallObj.transform.position = currentSpawnPosition;
        GameStateManager.isDead = false;
    }
    // END OF GAME STATE FUNCTIONS
    // This Function is the main function for moving the golf ball.
    // It applies a force in the camera's forward direction multiplied by the shot power, which is determined by the slider.
    public void MoveGolfBall() {
        GameStateManager.isIdle = false;
        GameStateManager.isAiming = false;
        GameStateManager.isShooting = true;
        GameStateManager.isDead = false;
        // Plays the golfBall Hit audio 
        audioManagerScript.BallHitAudio();
        // Adds Force to the golf ball multiplied by its shot power using its mass 
        rb.AddForce(golfBallDirection * shotPower / 3, ForceMode.Impulse);
        guiManagerScript.IncrementShotCounter();
    }
    // Triggers that when entered carry out specific tasks.
	private void OnTriggerEnter(Collider trigger) {
        // Enables the golf ball to travel vertically.
        if (trigger.gameObject.CompareTag("EnableVertical")) {
            // Unfreezes Constraint on the position y axis.
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        // Disables the golf ball's ability to travel vertically.
        else if (trigger.gameObject.CompareTag("DisableVertical")) {
            // Freezes Constraint on the position y axis.
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            // Sets Golf Balls y position to its default value in the case that it changes by a few values as floats arn't entirely accurate.
            cameraMovementScript.golfBallObj.transform.position = new Vector3(cameraMovementScript.golfBallObj.transform.position.x, 0.09829862f, cameraMovementScript.golfBallObj.transform.position.z);
        }
        // If Golf ball enters a deathZone trigger, isDead = true in GameStateManager.cs script.
        // This is used for out of bound shots.
        if (trigger.gameObject.CompareTag("DeathZone")) {
            GameStateManager.isDead = true;
        }
        // Pulls the golf ball slightly down into the hole when it enters the trigger.
        // Sets inHole = true in the GameStateManager.cs script.
        if (trigger.gameObject.CompareTag("Hole")) {
            // Pulls the Golf Ball into the hole if it collides with the holes trigger
            rb.AddForce(Vector3.down, ForceMode.Impulse);
            // Makes sure that the Golf club and Aiming indicator doesn't re-appear when the ball is inside the hole.
            guiManagerScript.clubObj.SetActive(false);
            guiManagerScript.arrowsObj.SetActive(false);
            // Plays audio when the ball enters the hole.
            audioManagerScript.HoleDropAudio();
            audioManagerScript.YayAudio();
            GameStateManager.inHole = true;
        } 
    }
}