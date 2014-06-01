using UnityEngine;
using System.Collections;

public class BlockBehaviorScript : MonoBehaviour {

	private Vector3 newPosition;

	// Use this for initialization
	void Start () {
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 2f);
	}

	/**
	 * Set the new position of the cube
	 */
	public void ChangePosition(Vector3 to) {
		newPosition = to;
	}

	void OnTriggerStay(Collider col) {
		Debug.Log("Trigger Event fired");

		Vector3 direction = transform.position - col.gameObject.transform.position;
		
		direction.y = 0f;
		
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
		{
			direction.z = 0f;
		}
		else
		{
			direction.x = 0f;
		}
		
		direction.Normalize();

		bool canSlide = true;
		RaycastHit rayHit;
		Ray ray = new Ray(transform.position, direction);
		Debug.DrawRay(transform.position, direction * 4.5f);

		if (Physics.Raycast(ray, out rayHit, 4.5f))
		{
			if (rayHit.collider.tag == "Block")
			{
				canSlide = false;
			}
		}

		if (Input.GetKeyDown(KeyCode.P) && canSlide)
		{
			ChangePosition(transform.position + direction * 3f);
		}
	}
}
