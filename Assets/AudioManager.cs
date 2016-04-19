using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip theme;

	void Awake () {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.0f;
		audioSource.clip = theme;
	}

	void Start() {
		audioSource.volume = 0.5f;
		audioSource.loop = true;
		audioSource.Play();
	}

	void Update () {
		
	}
}
