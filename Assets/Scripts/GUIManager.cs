using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This Class helps to manage all of the GUI present in the game.
// Referenced and used in the GameStateManager.cs script
public class GUIManager : MonoBehaviour {
    // External Scripts variables:
    [SerializeField]
    private GolfBallController golfBallControllerScript;
    [SerializeField]
    private AudioManager audioManagerScript;
    // GUI variables:
    [SerializeField]
    private Slider shotPowerSlider;
    [SerializeField]
    private Image shotPowerSliderImage;
    [SerializeField]
    private TextMeshProUGUI shotPowerTMP;
    // Public so variables can be accessed in the Win.cs script.
    [SerializeField]
    public TextMeshProUGUI shotCounterTMP;
    [SerializeField]
    public GameObject clubObj;
    [SerializeField]
    public GameObject arrowsObj;
    [HideInInspector]
    public int shotCounter = 0;
    // Private variables:
    private Color originalColor;

    // Initialises variables
    void Start() {
        shotPowerTMP.text = "0";
        originalColor = shotPowerSliderImage.color;
    }
    // Rotates the golf club on screen relative to the shot power value to add realism.
    void Update() {
        shotPowerTMP.text = shotPowerSlider.value.ToString();
        clubObj.transform.localEulerAngles = new Vector3(0f, 90f, shotPowerSlider.value);
    }
    // GAME STATE FUNCTIONS:
    // Re-enables the Shot Power Slider's Interactivity and resets its alpha back to it original value.
    // Called when isIdle = true in GameStateManager.cs script.
    public void EnableShotPowerSlider() {
        shotPowerSlider.interactable = true;
        shotPowerSliderImage.color = originalColor; 
        if (GameStateManager.inHole != true) {
            // Re-enable the golf club and aiming indicator's visibility.
            clubObj.SetActive(true);
            arrowsObj.SetActive(true);
        } 
    }
    // Disables Shot Power Slider so player cannot take another shot whilst the ball is in motion.
    // Called when isShooting = true in GameStateManager.cs script.
    public void DisableShotPowerSlider() {
        shotPowerSlider.interactable = false;
        shotPowerSliderImage.color = new Color32(255, 255, 255, 65);
    }
    // END OF GAME STATE FUNCTIONS
    // Increments the amount of shots taken and casts it to a string on the UI Canvas.
    // Called in MoveGolfBall() function inside the GolfBallController.cs script.
    public void IncrementShotCounter() {
        shotCounter += 1;
        shotCounterTMP.text = "Shots Taken: " + shotCounter.ToString();
    }
    // This function is called when the player takes their finger off the Shot Power Slider.
    // It makes the shot power equal to the sliders value from 0 - 100.
    // Called in an event trigger (inside the inspector) when the shot power slider has stopped being dragged .
    public void EndShotPowerDrag() {
        // Assigns the sliders value to be the balls power value.
        GolfBallController.shotPower = shotPowerSlider.value;
        // Calls the MoveGolfBall() function and resets the sliders handle to its original position.
        if (shotPowerSlider.value != 0) {
            golfBallControllerScript.MoveGolfBall();
            // Resets slider value back to its initial value.
            shotPowerSlider.value = 0;
            // Hide the Golf club and Aiming indicator when the ball is moving.
            clubObj.SetActive(false);
            arrowsObj.SetActive(false);
        }
    }
}