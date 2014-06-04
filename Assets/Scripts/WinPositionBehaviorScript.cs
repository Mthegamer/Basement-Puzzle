using UnityEngine;
using System.Collections;

public class WinPositionBehaviorScript : MonoBehaviour {

	private bool hasBlock;   //Does this position has a block on top of it.
	private float triggerDelay = 1f;
	private float triggerCount = 0f;

	//Used for initialization
	void Awake()
	{
		hasBlock = false;
	}

	/**
	 * This event is fire on trigger enter
	 */
	void OnTriggerStay(Collider col)
	{
		if (triggerCount >= triggerDelay)
		{
			if (col.gameObject.tag == "Block")
			{
				hasBlock = true;
			}
		}
		else
		{
			triggerCount += Time.deltaTime;
		}
	}

	/**
	 * This event is fired on trigger exit.
	 */
	void OnTriggerExit(Collider col)
	{
		hasBlock = false;
		triggerCount = 0f;
	}

	/**
	 * Returns if this winning position has a block on top of it or not.
	 */
	public bool GetHasBlock()
	{
		return hasBlock;
	}
}
