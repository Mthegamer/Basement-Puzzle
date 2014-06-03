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

	public float pushDelay = 3f;   //The delay to wait before you can push again
	public float speed = 2f;   //Speed of the slide

	private Vector3 newPosition;   //The new position, where the Block will slide to.
	private bool moving = false;   //Is the block moving
	private float pushTime = 0;   //Time counter for the moving delay

	// Use this for initialization
	void Start ()
	{
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);

		//If the block is moving, start the push delay counting.
		if (moving)
		{
			pushTime += Time.deltaTime;

			if (pushTime > pushDelay)
			{
				pushTime = 0;
				moving = false;
			}
		}
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
		if (col.gameObject.tag == "Player")
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

			//Push the Block in the calculated direction, if player presses the Push key and the block can slide and is not moving
			if (Input.GetKeyUp(KeyCode.F) && CanSlide(direction) && !moving)
			{
				moving = true;
				ChangePosition(transform.position + direction * 1f);
			}
		}
	}

	/**
	 * False is returned, if the block will hit another block or a solid property (walls and such).
	 */
	bool CanSlide(Vector3 direction)
	{
		bool canSlide = true;
		float length = 4f;

		RaycastHit rayHit;
		Ray ray = new Ray(transform.position, direction);
		
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
