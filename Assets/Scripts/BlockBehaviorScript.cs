using UnityEngine;
using System.Collections;

/**
 * This scripts will handle the behavior of the block.
 * A block can slide in either the x or the z directions.
 * Blocks however can't slide through other blocks or
 * solid properties (walls, tables and such)
 * 
 * @author Martin Rohwedder
 * @version 1.0
 */
public class BlockBehaviorScript : MonoBehaviour {

	public float speed = 2f;   //Speed of the slide

	private const KeyCode PUSH_KEY = KeyCode.P;
	private Vector3 newPosition;   //The new position, where the Block will slide to.

	// Use this for initialization
	void Start ()
	{
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
	}

	/**
	 * Set the new position of the cube
	 */
	void ChangePosition(Vector3 to)
	{
		newPosition = to;
	}

	/**
	 * Is fired when the player is colliding with the block
	 */
	void OnTriggerStay(Collider col)
	{
		//Calculate the direction to push the Block
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

		//Push the Block in the calculated direction, if player presses the Push key and the block can slide
		if (Input.GetKeyDown(PUSH_KEY) && CanSlide(direction))
		{
			ChangePosition(transform.position + direction * 3f);
		}
	}

	/**
	 * False is returned, if the block will hit another block or a solid property (walls and such)
	 */
	bool CanSlide(Vector3 direction)
	{
		bool canSlide = true;
		float length = 4f;

		RaycastHit rayHit;
		Ray ray = new Ray(transform.position, direction);
		Debug.DrawRay(transform.position, direction * length);
		
		if (Physics.Raycast(ray, out rayHit, length))
		{
			if (rayHit.collider.tag == "Block" || rayHit.collider.tag == "SolidProperty")
			{
				canSlide = false;
			}
		}

		return canSlide;
	}
}
