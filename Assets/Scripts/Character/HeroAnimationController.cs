using UnityEngine;
using System.Collections;

public class HeroAnimationController : MonoBehaviour {

	tk2dSpriteAnimator animator;
	bool isFlapping = false;

	void Awake() {
		this.animator = this.gameObject.GetComponent<tk2dSpriteAnimator>();
	}

	void Update() {
		if (this.rigidbody2D.velocity.magnitude > 5f && !this.isFlapping){
			this.isFlapping = true;
			this.animator.Play("flap");
		} else if (this.rigidbody2D.velocity.magnitude <= 5f && this.isFlapping){
			this.isFlapping = false;
			this.animator.StopAndResetFrame();
		}
	}

}
