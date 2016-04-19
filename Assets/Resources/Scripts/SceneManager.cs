using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	private bool canFade = false;

	void Update () {
		if (Input.anyKey) canFade = true;
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
