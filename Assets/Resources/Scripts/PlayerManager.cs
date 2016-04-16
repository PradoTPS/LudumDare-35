using UnityEngine;
using System.Collections;
using InputManager;

public class PlayerManager : MonoBehaviour {

	public KeyboardController Kcontroller;
	public float maxWalkSpeed;
	public bool canControl = true;
	public bool attacking;

	private bool invulnerable;

	Animator anim;
	Rigidbody2D rb;

	void Start() {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		invulnerable = false;
		maxWalkSpeed = 4;
	}

	void FixedUpdate() {
		float moveX = KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller);
		float moveY = KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller);

		if (moveX > 0.1f && canControl) {
			rb.velocity = new Vector2 (moveX * maxWalkSpeed, rb.velocity.y);
			//angle = 90;
		} 
		if (moveX < -0.1f && canControl) {
			rb.velocity = new Vector2 (moveX * maxWalkSpeed, rb.velocity.y);
			//angle = 270;
		} 
		if (moveX == 0 && canControl) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}
		if (moveY > 0.1f && canControl) {
			rb.velocity = new Vector2 (rb.velocity.x, moveY * maxWalkSpeed);
			//angle = 180;
		}
		if (moveY < -0.1f && canControl) {
			rb.velocity = new Vector2 (rb.velocity.x, moveY * maxWalkSpeed);
			//angle = 0;
		}
		if (moveY == 0 && canControl) {
			rb.velocity = new Vector2 (rb.velocity.x, 0);
		}
	}

	void ResetVulnerability () {
		invulnerable = false;
		this.GetComponent<SpriteRenderer> ().enabled = true;
	}

	void _blinkOn () {
		this.GetComponent<SpriteRenderer> ().enabled = true;
		if (invulnerable)
			Invoke ("_blinkOff", 0.07f);
		else
			ResetVulnerability ();
	}

	void _blinkOff () {
		this.GetComponent<SpriteRenderer> ().enabled = false;
		if (invulnerable)
			Invoke ("_blinkOn", 0.07f);
		else
			ResetVulnerability ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Bullet" + (Kcontroller.ToString() == "First" ? 2 : 1) && !invulnerable) {
			int pwrstt = this.GetComponentInChildren<GunManager> ().powerupState;
			this.GetComponentInChildren<GunManager> ().powerupState = pwrstt <= 1 ? 1 : pwrstt - 1;

			Destroy (col.gameObject);
			invulnerable = true;
			_blinkOn ();
			Invoke ("ResetVulnerability", 1.5f);
		}
	}
}
