using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// This Class carries out the Win State of each level when the Golf Ball has entered the hole.
// Referenced and used in the GameStateManager.cs script
public class Win : MonoBehaviour {
    // External Script variables:
    [SerializeField]
    private GUIManager guiManagerScript;
    // Serialize Field variables:
    [SerializeField]
    private GameObject nextLevelObj;
    [SerializeField]
    private GameObject completeAllLevelsObj;
    [SerializeField]
    private TextMeshProUGUI shotsTakenTMP;

    // This Function is called when the player presses the Next Level Button in either Level 1 or 2.
    // Is available to press when the golf ball is in the hole.
    public void LoadNextLevel() {
        // Loads next level
        SceneManager.LoadScene(LevelCounter.currentScene + 1);
    }
    // This Coroutine is called when inHole = true in the GameStateManager.cs script
    public IEnumerator InHole() {
        if (PlayerPrefs.GetInt("vibration") == 1) {
            // Vibrates phone if it is enabled in the options menu.
            Handheld.Vibrate();
        }
        // Waits 1.5 seconds so SFX can play - happens inside the GolfBallController.cs script when the ball enters the holes trigger collider.
        yield return new WaitForSeconds(1.5f);
        // Checks the current level scene and shows a UI panel accordingly.
        if (LevelCounter.currentScene >= 1 && LevelCounter.currentScene < 3) {
            // Show Next Level UI if it is Level 1 or 2.
            nextLevelObj.SetActive(true);
            shotsTakenTMP.text = "You Completed Level " + LevelCounter.currentScene + " in " + guiManagerScript.shotCounter + " Shots";
        } else {
            // Show Game Complete UI if it is level 3.
            completeAllLevelsObj.SetActive(true);
        }
    }
}