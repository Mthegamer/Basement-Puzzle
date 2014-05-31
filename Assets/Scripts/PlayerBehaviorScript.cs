using UnityEngine;
using System.Collections;

public class PlayerBehaviorScript : MonoBehaviour
{
	public float moveSpeed;
	public float turnSpeed;

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		MovePlayer(h, v);
	}

	/**
	 * Move the player around with the arrow keys (up or down) or w and s.
	 */
	void MovePlayer(float hor, float ver) {
		if (hor != 0f || ver != 0f)
		{
			RotatePlayer(hor, ver);
			transform.position += new Vector3(hor * moveSpeed * Time.deltaTime, 0.0f, ver * moveSpeed * Time.deltaTime);
		}
	}

	void RotatePlayer(float horizontal, float vertical) {
		Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSpeed * Time.deltaTime);
		rigidbody.MoveRotation(newRotation);
	}
}
