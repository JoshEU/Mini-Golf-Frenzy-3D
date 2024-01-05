using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// This Class gets the scene that the player is currently on and is accessed by other scripts/classes.
public class LevelCounter : MonoBehaviour {
    public static int currentScene;
    [SerializeField]
    private TextMeshProUGUI levelText;

    void Start() {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "Level: " + currentScene;
        // Unpause the game when the player returns to the Main menu through the Pause Menu and then selects a level.
        Time.timeScale = 1;
    }
}