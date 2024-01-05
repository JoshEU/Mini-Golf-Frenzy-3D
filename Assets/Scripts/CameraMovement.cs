using UnityEngine;

// This Class controls the aiming of the golfball through the use of GUI buttons on screen.
public class CameraMovement : MonoBehaviour { 
    // Set in the Main Menu: options menu - accessed inside the MainMenuManager.cs script
    public static float camSens = 75f; 
    [SerializeField]
    public GameObject golfBallObj;
    [SerializeField]
    private GameObject golfBallObjHorizontal;
    [SerializeField]
    private GameObject golfBallObjVertical;
    // Bools for button presses
    private bool isUpArrowDown = false;
    private bool isDownArrowDown = false;
    private bool isLeftArrowDown = false;
    private bool isRightArrowDown = false;

	// Checks for touch (hold) input and checks if inverted is toggled on in the Options menu.
    // Moves the Aiming indicator on screen accordingly whilst also adjusting the golf balls direction/
	private void Update() {
        // Movements/Rotations for Camera relative to the Golf Ball's position when GUI buttons are pressed down
        if (isUpArrowDown) {
            if (MainMenuManager.isInvertedOn == 1) {
                // Inverted
                GolfBallController.golfBallDirection = Vector3.left;
			} else {
                // Not Inverted
                GolfBallController.golfBallDirection = Vector3.right;
            }
            // Rotates the camera around the x axis upwards if the rotation is less than the max value (35) or is greater than the max value (35) - 1.
            if (golfBallObjVertical.transform.rotation.eulerAngles.x <= 35f ||golfBallObjVertical.transform.rotation.eulerAngles.x >= 360f - 7f - 1f) {
                golfBallObjVertical.transform.Rotate(GolfBallController.golfBallDirection, camSens * Time.deltaTime);
            }
        }
        else if (isDownArrowDown) {  
            if (MainMenuManager.isInvertedOn == 1) {
                // Inverted
                GolfBallController.golfBallDirection = Vector3.right;
			} else {
                // Not Inverted
                GolfBallController.golfBallDirection = Vector3.left;
            }
            // Rotates the camera around the x axis downwards if the rotation is greater than the min value (-7) or is greater than the max value (35) + 1.
            if (golfBallObjVertical.transform.rotation.eulerAngles.x >= 360f - 7f || golfBallObjVertical.transform.rotation.eulerAngles.x <= 35f + 1f)  {
                golfBallObjVertical.transform.Rotate(GolfBallController.golfBallDirection, camSens * Time.deltaTime);
            }
        }
        else if (isLeftArrowDown) {
            if (MainMenuManager.isInvertedOn == 1) {
                // Inverted
                GolfBallController.golfBallDirection = Vector3.up; // up
			} else {
                // Not Inverted
                GolfBallController.golfBallDirection = Vector3.down; // down
            }
            golfBallObjHorizontal.transform.Rotate(GolfBallController.golfBallDirection, camSens * Time.deltaTime);
        }
        else if (isRightArrowDown) {
            if (MainMenuManager.isInvertedOn == 1) {
                // Inverted
                GolfBallController.golfBallDirection = Vector3.down; // down
			} else {
                // Not Inverted
                GolfBallController.golfBallDirection = Vector3.up; // up
            }
            golfBallObjHorizontal.transform.Rotate(GolfBallController.golfBallDirection, camSens * Time.deltaTime);
        }
    }
    // Functions that check if the GUI Buttons are being pressed down or not.
    // Sets their bools accordingly so the correct movement/rotation of the camera can occur.
    public void UpArrowDown() {
        isUpArrowDown = true;
    }
    public void UpArrowUp() {
        isUpArrowDown = false;
    }
    public void DownArrowDown() {
        isDownArrowDown = true;
    }
    public void DownArrowUp() {
        isDownArrowDown = false;
    }
    public void LeftArrowDown() {
        isLeftArrowDown = true;
    }
	public void LeftArrowUp() {
        isLeftArrowDown = false;
    }
    public void RightArrowDown() {
        isRightArrowDown = true;
    }
    public void RightArrowUp() {
        isRightArrowDown = false;
    }
}