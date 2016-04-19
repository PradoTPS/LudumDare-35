using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InputManager;

public class GunManager : MonoBehaviour {

	public KeyboardController Kcontroller;
	public Image countdownBar;
	Color countdownColor = Color.green;
	public Text powerupTxt;

	private float lastUpdate;
	private float horizontal;
	private float vertical;

	public string textColor;

	private int shootDir;
	private int doubleShootDir;
	public  int powerupState;
	private int singleShootSpeed;
	private Vector2 vectorShootSpeed;
	private int countdown;

	private GameObject singleShoot;
	public GameObject gun;
	public GameObject player;

	public AudioClip shoot;

	private AudioSource audioSource;

	int getShootDir () {  return shootDir;  }

	void Awake () {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = 0.0f;
		audioSource.clip = shoot;
	}

	void Start() {
		powerupState = 3;
		singleShoot = Resources.Load ("Prefabs/SingleShoot") as GameObject;
		singleShootSpeed = 10;
		vectorShootSpeed = new Vector2 (0, 0);
		shootDir = 1;
	}

	void Update () {
		Pivot ();
		Shoot (powerupState);
		if (Time.time - lastUpdate >= 0.05f) {
			countdown = (countdown > 0) ? countdown - 1 : 0;
			lastUpdate = Time.time;
		}
		checkCountdownBar ();
		powerupTxt.text = "<color=#" + textColor + ">Powerup State:</color> " + powerupState;
	}

	void Pivot() {

		if (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller) == 1) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
			shootDir = 1;
		} else if (KCI.GetAxisRaw (KeyboardAxis.Horizontal, Kcontroller) == -1) {
			transform.rotation = Quaternion.Euler (0, 0, 180);
			shootDir = 2;
		}  else if (KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller) == 1) {
			transform.rotation = Quaternion.Euler (0, 0, 90);
			shootDir = 3;
		}  else if (KCI.GetAxisRaw (KeyboardAxis.Vertical, Kcontroller) == -1) {
			transform.rotation = Quaternion.Euler (0, 0, 270);
			shootDir = 4;
		}
	}

	void Shoot(int powerup) {
		if (KCI.GetButtonDown (KeyboardButton.Action, Kcontroller) && countdown == 0 && powerup > 0) {
			countdown = 10;
			audioSource.volume = 0.5f;
			audioSource.loop = false;
			audioSource.Play();
			switch (powerup) {
				case 1:
					SingleShoot (getShootDir());
					break;
				case 2:
					doubleShootDir = getShootDir ();
					DoubleShoot ();
					break;
				case 3:
					FourDirShoot ();
					break;
			}
		}
	}

	void SingleShoot(int dir) {
		int direction = dir;
		GameObject tempShoot = Instantiate (singleShoot, gun.transform.position, Quaternion.identity) as GameObject;
		tempShoot.tag = "Bullet" + (Kcontroller.ToString() == "First" ? 1 : 2);
		switch (direction) {
			case 1:
				vectorShootSpeed = new Vector2(singleShootSpeed, 0);
				break;
			case 2:
				vectorShootSpeed = new Vector2(-singleShootSpeed, 0);
				break;
			case 3:
				vectorShootSpeed = new Vector2(0, singleShootSpeed);
				break;
			case 4:
				vectorShootSpeed = new Vector2(0, -singleShootSpeed);
				break;
		}
		tempShoot.GetComponent<Rigidbody2D> ().velocity = vectorShootSpeed;
	}

	void InvokableSingleShoot() {
		SingleShoot (doubleShootDir);
	}

	void DoubleShoot() {
		SingleShoot (doubleShootDir);
		Invoke ("InvokableSingleShoot", 0.3f);
	}

	void FourDirShoot() {
		for (int i = 1; i < 5; i++) SingleShoot (i);
	}

	void checkCountdownBar() {
		countdownBar.rectTransform.localScale = new Vector3 ((float)countdown / 10f, countdownBar.rectTransform.localScale.y, countdownBar.rectTransform.localScale.z);
		if (countdown >= 5.0) {
			countdownColor.r = 2.0f - (2.0f * countdown) / 10.0f;
		} else if (countdown > 0.0f) {
			countdownColor.g = 2.0f * countdown / 10.0f;
		} else if (countdown == 0) {
			countdownColor = Color.green;
		}
		countdownBar.color = countdownColor;
	}
}
