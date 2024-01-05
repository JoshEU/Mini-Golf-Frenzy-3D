using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// This Class contains all the functionality for the Main Menu (including the Options Menu & Level Select Menu).
public class MainMenuManager : MonoBehaviour {
    // Serialize Field variables:
    [SerializeField]
    private Toggle vibrationToggle;
    [SerializeField]
    private Toggle invertedToggle;
    [SerializeField]
    private Slider cameraSensSlider;
    [SerializeField]
    private TextMeshProUGUI camSensValueTMP;
    // Miscellaneous variables:
    private int isVibrationOn = 0;
    // Accessed inside the CameraMovement.cs script
    public static int isInvertedOn = 0;
    
    // Initialises default values for options inside the Options Menu.
    void Start() {
        // Casts from bool to int - checks if the vibration toggle is on
        isVibrationOn = vibrationToggle.isOn == true ? 1 : 0;
        // Casts from bool to int - checks if the inverted toggle is on
        isInvertedOn = invertedToggle.isOn == true ? 1 : 0;

        // Checks for any previously saved data on the options inside the options menu and if there is none, sets a default value for each.
        if (!PlayerPrefs.HasKey("vibration")) {
            PlayerPrefs.SetInt("vibration", 0);
            SetVibrationToggle();
		} else {
            GetVibrationToggle();
		}
		if (!PlayerPrefs.HasKey("inverted")) {
            PlayerPrefs.SetInt("inverted", 0);
            SetInvertedToggle();
		} else {
            GetInvertedToggle();
		}
        if (!PlayerPrefs.HasKey("cameraSensitivity")) {
            PlayerPrefs.SetFloat("cameraSensitivity", 75f);
            SetCameraSensitivity();
        } else {
            GetCameraSensitivity();
        }
    }
	private void Update() {
        // Alters the aiming/camera sensitivity based on the value of the slider.
        camSensValueTMP.text = cameraSensSlider.value.ToString();
    }
	// Main Menu: level select menu functionality:
	public void LoadLevelOne() {
        SceneManager.LoadScene(1);
	}
    public void LoadLevelTwo() {
        SceneManager.LoadScene(2);
    }
    public void LoadLevelThree() {
        SceneManager.LoadScene(3);
    }
    // Main Menu Functionality:
    public void QuitButton() {
        // Quits in the Editor
        UnityEditor.EditorApplication.isPlaying = false;

        // Terminates the game once it's built
        Application.Quit();
    }
    // Option Menu functionality:
    // This Function loads the vibration toggle bool state from Player preferences.
    private void GetVibrationToggle() {
        vibrationToggle.isOn = PlayerPrefs.GetInt("vibration") == 1 ? true : false;
    }
    // This Function saves the Vibration Toggle bool state inside Player preferences.
    private void SetVibrationToggle() {
        PlayerPrefs.SetInt("vibration", isVibrationOn);
    }
    // This Function loads the Inverted Toggle bool state from Player preferences.
    private void GetInvertedToggle() {
        // 
        invertedToggle.isOn = PlayerPrefs.GetInt("inverted") == 1 ? true : false;
    }
    // This Function saves the Inverted Toggle bool state inside Player preferences.
    private void SetInvertedToggle() {
        PlayerPrefs.SetInt("inverted", isInvertedOn);
    }
    // This Function loads the Camera Sensitivity value from Player preferences.
    private void GetCameraSensitivity() {
        cameraSensSlider.value = PlayerPrefs.GetFloat("cameraSensitivity");
    }
    // This Function saves the Camera Sensitivity value inside Player preferences.
    private void SetCameraSensitivity() {
        PlayerPrefs.SetFloat("cameraSensitivity", cameraSensSlider.value);
    }
    // This Function will Vibrate the phone if isVibrationOn is toggled ON in the Options Menu.
    public void ChangeVibrationState() {
        if (vibrationToggle.isOn) {
            isVibrationOn = 1;
		} else {
            isVibrationOn = 0;
		}
        SetVibrationToggle();
	}
    // This Function will Invert the Camera movement controls if isInvertedOn is toggled ON in the Options Menu.
    public void ChangeInvertedState() {
        isInvertedOn = invertedToggle.isOn.GetHashCode();
        SetInvertedToggle();
    }
    // This Function will change the Camera Sensitivity in game based on the slider value set inside the Options Menu.
    public void ChangeCameraSensitivity() {
        CameraMovement.camSens = cameraSensSlider.value;
        SetCameraSensitivity();
	}
}