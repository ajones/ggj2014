using UnityEngine;
using System.Collections;

public class HeroAnimationController : MonoBehaviour {

	tk2dSpriteAnimator animator;
	bool isAnimating = true;

	bool isFlapping = false;
	bool isWalking = false;
	bool isGrounded = false;
	bool isWaiting = false;

	float sitDelay = 1.0f;

	void Awake() {
		this.animator = this.gameObject.GetComponent<tk2dSpriteAnimator>();
		this.animator.Play("walk");
	}

	void Update() {
		if (this.isGrounded){
			if (this.isFlapping){
				this.animator.Play("stand");
				this.isWalking = false;
				this.isFlapping = false;
			} else if (this.rigidbody2D.velocity.magnitude > 1f && !this.isWalking ){
				if (this.isWaiting){
					StopCoroutine("waitThenSit");
					this.isWaiting = false;
				}

				this.animator.Play("walk");
				this.isWalking = true;
				this.isFlapping = false;

			} else if (this.rigidbody2D.velocity.magnitude <= 1f && this.isWalking){
				if (!this.isWaiting){
					this.animator.Play("stand");
					StartCoroutine("waitThenSit");
					this.isWaiting = true;
					this.isWalking = false;
				}
			}
		} else {
			if (!this.isFlapping){
				this.animator.Play("flap");
				this.isFlapping = true;
				this.isWalking = false;
				this.isWaiting = false;
			} 
		}


		if (Input.GetKeyDown ("g")){
			this.animator.Play("grab");
			this.isFlapping = false;
			this.isWalking = false;
			this.isWaiting = false;
		}
	}


	void OnCollisionEnter2D(Collision2D theCollision){
		if(theCollision.gameObject.tag == "floor") {
			this.isGrounded = true;
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
		}
	}

	IEnumerator waitThenSit() {
		yield return new WaitForSeconds(sitDelay);

		if (this.isWaiting){
			this.animator.Play("sitDown");
			this.isWalking = false;
			this.isFlapping = false;
			this.isGrounded = true;
			this.isWaiting = false;
		}

	}



}
