using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(tk2dSprite))]
public class SimpleDuckController : MonoBehaviour {

	public float xRangeFromStart;
	public float movementSpeed;
	public float turnDelay;
	public Direction direction;

	public enum Direction {
		RANDOM, LEFT, RIGHT,
	}

	private bool turning;
	private float turnTime;
	private Rigidbody2D rBody;
	private tk2dSprite sprite;
	private Transform myTransform;
	private Vector3 startingPos;
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<tk2dSprite> ();
		rBody = rigidbody2D;
		myTransform = transform;
		startingPos = myTransform.position;
		if (direction == Direction.RANDOM) {
			direction = (Direction) Random.Range(1, 3);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == Direction.LEFT && !turning) {
			rBody.AddForce (new Vector2 (movementSpeed * -1, 0));
			sprite.FlipX = true;
			if (myTransform.position.x <= startingPos.x - xRangeFromStart) turning = true;
		} else if (direction == Direction.RIGHT && !turning) {
			rBody.AddForce (new Vector2 (movementSpeed, 0));
			sprite.FlipX = false;
			if (myTransform.position.x > startingPos.x + xRangeFromStart) turning = true;
		} else if (turning) {
			rBody.velocity = Vector3.zero;
			turnTime += Time.deltaTime;

			if (turnTime >= turnDelay) {
				turnTime = 0;
				turning = false;
				if (direction == Direction.LEFT) {
					direction = Direction.RIGHT;
				} else {
					direction = Direction.LEFT;
				}
			}
		}
	}
}
