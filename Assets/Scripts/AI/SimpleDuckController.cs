using UnityEngine;
using System.Collections;

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
	private tk2dSprite sprite;
	private Transform myTransform;
	private Vector3 startingPos;
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<tk2dSprite> ();
		myTransform = transform;
		startingPos = myTransform.position;
		if (direction == Direction.RANDOM) {
			direction = (Direction) Random.Range(1, 3);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// move back and forth
		if (direction == Direction.LEFT && !turning) {
			transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
			sprite.FlipX = true;
			if (myTransform.position.x <= startingPos.x - xRangeFromStart) {
				turning = true;
				direction = Direction.RIGHT;
			}
		} else if (direction == Direction.RIGHT && !turning) {
			transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
			sprite.FlipX = false;
			if (myTransform.position.x > startingPos.x + xRangeFromStart) {
				turning = true;
				direction = Direction.LEFT;
			}
		} else if (turning) {
			
			turnTime += Time.deltaTime;

			if (turnTime >= turnDelay) {
				turnTime = 0;
				turning = false;
			}
		}
	}
}
