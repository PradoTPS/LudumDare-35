using UnityEngine;
using System.Collections;

public class BorderManager : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Bullet1" || col.tag == "Bullet2") {
			Destroy (col.gameObject);
		}
	}
}
