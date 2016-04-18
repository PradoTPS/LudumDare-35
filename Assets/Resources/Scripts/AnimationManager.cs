using UnityEngine;
using System.Collections;
using InputManager;


public class AnimationManager : MonoBehaviour {

	public KeyboardController Kcontroller;
	public GameObject pivot;
	bool iddle;

	void Update () {
	
		float moveX = KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller);
		float moveY = KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller);
		float direction = pivot.transform.rotation.eulerAngles.z;

		iddle = (moveX == 0 && moveY == 0);

		if (iddle) {
			
			if (direction > 0) {
				GetComponent<Animator> ().Play ("IddleBack");			
			} else {
				GetComponent<Animator> ().Play ("Iddle");
			}

		} else {
			
			if (moveX != 0 && moveY == 0) {
				if (moveX > 0) {
					GetComponent<Animator> ().Play ("WalkingRight");
				} else {
					GetComponent<Animator> ().Play ("WalkingLeft");
				}
			}
		}
	}
}