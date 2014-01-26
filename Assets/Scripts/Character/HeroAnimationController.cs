using UnityEngine;
using System.Collections;

public class HeroAnimationController : MonoBehaviour {

	enum State
	{
		FLAPPING,
		WALKING,
		WAITING,
		FLOATING
	}; 

	State currentState = State.FLOATING;

	tk2dSpriteAnimator animator;

	bool isGrounded = false;
	bool isInLake = false;
	bool isSitting = false;
	public bool isOnRightWall = false;
	public bool isOnLeftWall = false;

	float sitDelay = 1.0f;
	
	void Awake() {
		this.animator = this.gameObject.GetComponent<tk2dSpriteAnimator>();
		this.animator.Play("float");
	}
	
	void Update() {
		// Debug.Log ("AM I ON A WALL?: " + this.isOnLeftWall + " : " + this.isOnRightWall);
		// Debug.Log ("Current state is " + currentState + " || isInLake : " + this.isInLake + " || isGrounded : " + this.isGrounded);
		if (this.isGrounded) {
			if (currentState == State.FLAPPING) {
				currentState = State.WAITING;
				this.animator.Play("stand");
				StartCoroutine("waitThenSit");
			} else if (this.rigidbody2D.velocity.magnitude > 1f && currentState != State.WALKING) {
				if (currentState == State.WAITING) {
					StopCoroutine("waitThenSit");
				}
				currentState = State.WALKING;
				this.animator.Play("walk");
			} else if (this.rigidbody2D.velocity.magnitude <= 1f && currentState == State.WALKING) {
				this.animator.Play("stand");
				StartCoroutine("waitThenSit");
				currentState = State.WAITING;
			}

			if (this.rigidbody2D.velocity.magnitude <= 1f && currentState == State.WAITING) {
				if (Input.GetKeyDown ("g")){
					this.animator.Play("grab");
				}
			}
		} else if (this.isInLake) {
			if (currentState != State.FLOATING) {
				this.animator.Play("float");
				currentState = State.FLOATING;
			}
		} else {
			if (currentState != State.FLAPPING) {
				this.animator.Play("flap");
				currentState = State.FLAPPING;
			}
		}
		

	}

	void OnTriggerEnter2D(Collider2D theCollision) {
		if (theCollision.gameObject.tag.Equals("lake")) {
			this.isInLake = true;
			this.isGrounded = false;
		}
	}
	
	void OnTriggerStay2D(Collider2D theCollision) {
		if (theCollision.gameObject.tag == "lake") {
			this.isInLake = true;
			this.isGrounded = false;
		}
	}
	
	void OnTriggerExit2D(Collider2D theCollision) {
		if (theCollision.gameObject.tag == "lake") {
			this.isInLake = false;
		}
	}
	
	
	void OnCollisionEnter2D(Collision2D theCollision){
		if(theCollision.gameObject.tag == "floor") {
			this.isGrounded = true;
			this.isInLake = false;
		} else if (theCollision.gameObject.tag == "rightWall") {
			this.isOnRightWall = true;
			this.isOnLeftWall = false;
		} else if (theCollision.gameObject.tag == "leftWall") {
			this.isOnLeftWall = true;
			this.isOnRightWall = false;
		}
	}
	
	void OnCollisionExit2D(Collision2D theCollision){ 
		if(theCollision.gameObject.tag == "floor") {
			this.isGrounded = false;
		} else if (theCollision.gameObject.tag == "rightWall") {
			this.isOnRightWall = false;
			this.isOnLeftWall = false;
		} else if (theCollision.gameObject.tag == "leftWall") {
			this.isOnLeftWall = false;
			this.isOnRightWall = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D theCollision) {
		//Debug.Log ("colliding with " + theCollision.gameObject.tag); 
		if(theCollision.gameObject.tag == "floor") {
			this.isGrounded = true;
			this.isInLake = false;
		} else if (theCollision.gameObject.tag == "rightWall") {
			this.isOnRightWall = true;
			this.isOnLeftWall = false;
		} else if (theCollision.gameObject.tag == "leftWall") {
			this.isOnLeftWall = true;
			this.isOnRightWall = false;
		}
	}
	
	IEnumerator waitThenSit() {
		yield return new WaitForSeconds(sitDelay);
		if (currentState == State.WAITING){
			this.animator.Play("sitDown");
		}
		
	}
	
	
	
}
