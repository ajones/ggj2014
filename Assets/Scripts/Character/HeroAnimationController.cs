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

	float sitDelay = 1.0f;
	
	void Awake() {
		this.animator = this.gameObject.GetComponent<tk2dSpriteAnimator>();
		this.animator.Play("float");
	}
	
	void Update() {
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
			int sound = Random.Range (0, 2);
			SoundManager.getInstance().playSoundEffect("splash" + sound);
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
		}
	}
	
	void OnCollisionExit2D(Collision2D theCollision){ 
		if(theCollision.gameObject.tag == "floor") {
			this.isGrounded = false;
		}
	}
	
	void OnCollisionStay2D(Collision2D theCollision) {
		if(theCollision.gameObject.tag == "floor") {
			this.isGrounded = true;
			this.isInLake = false;
		}
	}
	
	IEnumerator waitThenSit() {
		yield return new WaitForSeconds(sitDelay);
		if (currentState == State.WAITING){
			this.animator.Play("sitDown");
		}
		
	}
	
	
	
}
