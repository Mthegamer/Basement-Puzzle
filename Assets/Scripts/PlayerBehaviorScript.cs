using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviorScript : MonoBehaviour {

	public float moveSpeed = 8.0f;
	public float turnSpeed = 140.0f;

	private CharacterController characterController;

	void Start() {
		characterController = gameObject.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		MovePlayer();
		RotatePlayer();
	}

	/**
	 * Move the player around with the arrow keys (up or down) or w and s.
	 */
	void MovePlayer() {
		characterController.Move(Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * moveSpeed * Time.deltaTime);
	}

	/**
	 * Rotate the player around with the arrow keys (left and right) or d and a.
	 */
	void RotatePlayer() {
		transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0));
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {

		if (Input.GetKeyDown(KeyCode.P))
		{
			Vector3 newPosition = new Vector3(hit.gameObject.transform.position.x, hit.gameObject.transform.position.y, hit.gameObject.transform.position.z + 4);
			hit.gameObject.GetComponent<BlockBehaviorScript>().changePosition(newPosition);
		}
	}
}
