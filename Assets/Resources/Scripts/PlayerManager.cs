using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InputManager;

public class PlayerManager : MonoBehaviour {

	public KeyboardController Kcontroller;
	public float maxWalkSpeed;
	public bool canControl = true;
	public bool attacking;
	public AudioClip chicken;

	private AudioSource audioSource;
	private bool invulnerable;

	Animator anim;
	Rigidbody2D rb;

	void Awake () {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.0f;
		audioSource.clip = chicken;
	}

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
		if (moveX == 0) {
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}
		if (moveY > 0.1f && canControl) {
			rb.velocity = new Vector2 (rb.velocity.x, moveY * maxWalkSpeed);
			//angle = 180;
		}
		if (moveY < -0.1f && canControl) {
			rb.velocity = new Vector2 (rb.velocity.x, moveY * maxWalkSpeed);
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

	void EndGame() {
		Application.LoadLevel (2);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Bullet" + (Kcontroller.ToString().ToLower() == "first" ? 2 : 1) && !invulnerable) {
			int pwrstt = this.GetComponentInChildren<GunManager> ().powerupState;
			this.GetComponentInChildren<GunManager> ().powerupState = pwrstt <= 1 ? 0 : pwrstt - 1;

			audioSource.volume = 0.5f;
			audioSource.loop = false;
			audioSource.Play();

			if (this.GetComponentInChildren<GunManager> ().powerupState == 0) {
				Invoke ("ResetVulnerability", 1.5f);
				this.gameObject.GetComponent<PlayerManager> ().canControl = false;
				Text winText = GameObject.FindGameObjectWithTag ("winText").GetComponent<Text> ();
				winText.text = "Player " + (Kcontroller.ToString ().ToLower() == "first" ? 2 : 1) + " wins!";
				Invoke ("EndGame", 5f);
			} else {
				Destroy (col.gameObject);
				invulnerable = true;
				_blinkOn ();
				Camera.main.GetComponent<ShakeCamera> ().DoShake (0.3f, 0.02f);
				Invoke ("ResetVulnerability", 1.5f);
			}
		}
	}
}
