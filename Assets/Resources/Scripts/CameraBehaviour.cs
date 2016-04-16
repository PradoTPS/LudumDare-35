using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	#region Properties
	Camera usableCamera;
	private Transform[] targets;

	public Vector2 minimunBounds;
	public Vector2 maximunBounds;

	public float boundBorder;
	public float minimumOrthographicSize;
	public float zoomSpeed;
	#endregion

	#region Methods
	void Awake() {
		usableCamera = GetComponent<Camera> ();
		usableCamera.orthographic = true;

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		int numberOfPlayers = players.Length;

		targets = new Transform[numberOfPlayers];
		for (int i = 0; i < numberOfPlayers; i++) {
			targets [i] = players [i].transform;
		}
	}

	void LateUpdate() {
		Rect boundingBox = CalculateTargetsBoundingBox ();
		transform.position = CalculateCameraPosition (boundingBox);
		usableCamera.orthographicSize = CalculateOrthographicSize (boundingBox);
	}

	Rect CalculateTargetsBoundingBox() {
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;

		foreach (Transform target in targets) {
			Vector3 position = target.position;

			minX = Mathf.Min (minX, position.x);
			minY = Mathf.Min (minY, position.y);
			maxX = Mathf.Max (maxX, position.x);
			maxY = Mathf.Max (maxY, position.y);
		}

		return Rect.MinMaxRect (minX - boundBorder, maxY + boundBorder, maxX + boundBorder, minY - boundBorder);
	}

	Vector3 CalculateCameraPosition(Rect boundingBox) {
		Vector2 boundingBoxCenter = boundingBox.center;

		return new Vector3 (Mathf.Clamp(boundingBoxCenter.x, minimunBounds.x, maximunBounds.x), Mathf.Clamp(boundingBoxCenter.y, minimunBounds.y, maximunBounds.y), GetComponent<Camera>().transform.position.z);
	}

	float CalculateOrthographicSize(Rect boundingBox) {
		float orthographicSize = usableCamera.orthographicSize;
		Vector3 topRight = new Vector3 (boundingBox.x + boundingBox.width, boundingBox.y, 0f);
		Vector3 topRigthAsViewport = usableCamera.WorldToViewportPoint (topRight);

		if (topRigthAsViewport.x >= topRigthAsViewport.y) {
			orthographicSize = Mathf.Abs (boundingBox.width) / usableCamera.aspect / 2f;
		} else {
			orthographicSize = Mathf.Abs (boundingBox.height) / 2f;
		}

		return Mathf.Clamp (Mathf.Lerp (usableCamera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
	}
	#endregion
}