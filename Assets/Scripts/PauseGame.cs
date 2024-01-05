using UnityEngine;
using UnityEngine.SceneManagement;

// This Class contains all the functionality for the Pause Menu and also pauses the game on a GUI button press.
public class PauseGame : MonoBehaviour {
    // External Script variables:
    [SerializeField]
    private LevelCounter levelCounterScript;
    // Serialize Field variables:
    [SerializeField]
    private GameObject pauseObj;
    [SerializeField]
    private GameObject gameGUIObj;

    // Pauses gameplay and displays the Pause Menu panel when the player presses the Pause button.
    public void Pause() {
        Time.timeScale = 0;
        pauseObj.SetActive(true);
        gameGUIObj.SetActive(false);
	}
    // Pause Menu GUI functionality:
    // Un-pauses gameplay and hides the Pause Menu panel when the player presses the Resume button.
    public void Resume() {
        Time.timeScale = 1;
        pauseObj.SetActive(false);
        gameGUIObj.SetActive(true);
    }
    // Return to MainMenu scene.
    public void MainMenu() {
        SceneManager.LoadScene(0);
	}
    // Reloads current level from beginning.
    public void RestartLevel() {
        SceneManager.LoadScene(LevelCounter.currentScene);
        // Checks if the game is paused and unpauses accordingly - it will be paused as you must pause the game in order to click the restart button.
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
		}
    }
    // Used only in Level 3 on the Win screen - resets player back to level 1 if they hit Restart GUI button.
    public void RestartGame() {
        SceneManager.LoadScene("Level1");
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }
}