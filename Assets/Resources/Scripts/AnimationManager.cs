using UnityEngine;
using System.Collections;
using InputManager;


public class AnimationManager : MonoBehaviour {

	public KeyboardController Kcontroller;
	public GameObject pivot;
	private bool idle;
	private float lastPivotDir;

	void Update () {
	
		float moveX = KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller);
		float moveY = KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller);
		float pivotDir = pivot.transform.rotation.eulerAngles.z;
		string dir = (-1 < pivotDir && pivotDir < 1) ? "right" : (179 < pivotDir && pivotDir < 181) ? "left" : "left";

		print (dir);

		idle = (moveX == 0 && moveY == 0);

		if (idle) {
			if (pivotDir > 0) {
				GetComponent<Animator> ().Play ("IdleBack");			
			} else {
				GetComponent<Animator> ().Play ("Idle");
			}
		} else {
			if (moveX != 0 && moveY == 0) {
				if ((Input.GetKey (KeyCode.A) && Kcontroller.ToString() == "First") || (Input.GetKey (KeyCode.LeftArrow) && Kcontroller.ToString() == "Second")) {
					GetComponent<Animator> ().Play ("WalkingLeft");
				} else if ((Input.GetKey (KeyCode.D) && Kcontroller.ToString() == "First") || (Input.GetKey (KeyCode.RightArrow) && Kcontroller.ToString() == "Second")) {
					GetComponent<Animator> ().Play ("WalkingRight");
				}
			} else if (moveX == 0 && moveY != 0) {
				if (moveY > 0) {
					if (dir == "right" ) { GetComponent<Animator> ().Play ("WalkingUpRight"); }
					if (dir == "left") { GetComponent<Animator> ().Play ("WalkingUpLeft"); }
				} else {
					if (dir == "right") { GetComponent<Animator> ().Play ("WalkingRight"); }
					if (dir == "left") { GetComponent<Animator> ().Play ("WalkingLeft"); }
				}
			}
		}
	}
}