using UnityEngine;
using System.Collections;

public class BobbingController : MonoBehaviour {

	public float speed;
	public float delay;
	public float depth;
	private BobDirection direction = BobDirection.DOWN;

	public enum BobDirection {
		UP, DOWN
	}

	private bool isDelayed;
	private float delayedTime;
	private Vector3 startingPosition;
	private Vector3 newPosition;
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		startingPosition = myTransform.position;
	}
	
	// Update is called once per frame
	void Update() {
		if (direction == BobDirection.DOWN && !isDelayed) {
			myTransform.Translate (0, speed * Time.deltaTime, 0);
			if (myTransform.position.y >= startingPosition.y) {
				isDelayed = true;
				newPosition = new Vector3 (myTransform.position.x, startingPosition.y, myTransform.position.z);
				direction = BobDirection.UP;
				myTransform.position = newPosition;
			}
		} else if (direction == BobDirection.UP && !isDelayed) {
			myTransform.Translate (0, -speed * Time.deltaTime, 0);
			if (myTransform.position.y <= startingPosition.y - depth) {
				isDelayed = true;
				newPosition = new Vector3 (myTransform.position.x, startingPosition.y - depth, myTransform.position.z);
				direction = BobDirection.DOWN;
				myTransform.position = newPosition;
			}
		} else if (isDelayed) {
			delayedTime += Time.deltaTime;
			if (delayedTime >= delay) {
				delayedTime = 0;
				isDelayed = false;
			}
		}
	}
}
