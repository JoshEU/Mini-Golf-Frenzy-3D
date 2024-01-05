using System.Collections;
using UnityEngine;

// This Class manages all audio within the game in which other classes can call functions from.
public class AudioManager : MonoBehaviour {
    // Audio Source variables:
    [SerializeField]
    private AudioSource ballHit;
    [SerializeField]
    private AudioSource holeDrop;
    [SerializeField]
    private AudioSource yayCelebration;

    // This Coroutine delays the time it takes for the yay audio to play - so it can be played after the holeDrop audio and before the NextLevel_Obj panel appears on screen.
    private IEnumerator yayDelayAudio() {
        yield return new WaitForSeconds(1f);
        yayCelebration.Play();
    }
    public void BallHitAudio() {
        ballHit.Play();
	}
    public void HoleDropAudio() {
        holeDrop.Play();
	}
    public void YayAudio() {
        StartCoroutine(yayDelayAudio());
	}
}