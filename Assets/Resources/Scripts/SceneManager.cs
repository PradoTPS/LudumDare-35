using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	private bool canFade = false;

	void Update () {
		if (Input.anyKey) canFade = true;
		if (canFade) {
			if (Camera.main.GetComponent<ScreenTransitionImageEffect> ().maskValue < 1f)
				Camera.main.GetComponent<ScreenTransitionImageEffect> ().maskValue += 0.02f;
			else
				Application.LoadLevel (1);
		}
	}
}
