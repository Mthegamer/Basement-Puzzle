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
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
	}

	public void changePosition(Vector3 to) {
		newPosition = to;
	}
}
