using UnityEngine;
using System.Collections;

namespace InputManager {
	#region Enumerations
	public enum KeyboardController {
		First = 0,
		Second = 1
	}

	public enum KeyboardAxis {
		Horizontal,
		Vertical
	}

	public enum KeyboardButton {
		Jump,
		Action,
		Launch
	}
	#endregion

	public class KCI : MonoBehaviour {
		#region Methods
		public static float GetAxis(KeyboardAxis axis, KeyboardController player) {
			return Input.GetAxis ("Player" + ((int)player + 1).ToString() + axis.ToString());
		}

		public static float GetAxisRaw(KeyboardAxis axis, KeyboardController player) {
			return Input.GetAxisRaw ("Player" + ((int)player + 1).ToString() + axis.ToString());
		}

		public static bool GetButton(KeyboardButton button, KeyboardController player) {
			return Input.GetButton ("Player" + ((int)player + 1).ToString() + button.ToString());
		}

		public static bool GetButtonDown(KeyboardButton button, KeyboardController player) {
			return Input.GetButtonDown ("Player" + ((int)player + 1).ToString() + button.ToString());
		}

		public static bool GetButtonUp(KeyboardButton button, KeyboardController player) {
			return Input.GetButtonUp ("Player" + ((int)player + 1).ToString() + button.ToString());
		}
		#endregion
	}
}