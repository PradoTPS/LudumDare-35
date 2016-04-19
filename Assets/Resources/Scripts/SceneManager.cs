using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	private bool canFade = false;
	private bool hasPlayed = false;
	private AudioSource audioSource;
	public AudioClip button;

	void Awake () {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = button;
		audioSource.loop = false;
		audioSource.volume = 0.0f;
	}

	void playOnce () {
		audioSource.volume = 0.5f;
		hasPlayed = false;
		audioSource.Play();
	}

	void Update () {
		if (Input.anyKey) {
			canFade = true;
			hasPlayed = true;
			playOnce ();
		}

		if (canFade) {
			if (Camera.main.GetComponent<ScreenTransitionImageEffect> ().maskValue < 1f) {
				Camera.main.GetComponent<ScreenTransitionImageEffect> ().maskValue += 0.02f;
			} else {
				if (Application.loadedLevel == 2) {
					Application.LoadLevel (0);
				} else {
					Application.LoadLevel (1);
				}
			}
		}
	}
}
