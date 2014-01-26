using UnityEngine;
using System.Collections;

public class InteractionArea : MonoBehaviour {

	public InteractionTarget interactionTarget;

	void OnTriggerEnter2D(Collider2D theCollider){
		if(theCollider.gameObject.tag == "hero") {
			if (this.interactionTarget != null){
				this.beginInteraction();
			}
		}
	}
	public virtual void beginInteraction() {
		this.interactionTarget.beginInteraction("");
	}

	
	void OnTriggerExit2D(Collider2D theCollider){ 
		if(theCollider.gameObject.tag == "hero") {
			this.stopInteraction();
		}
	}
	public virtual void stopInteraction() {
		this.interactionTarget.stopInteraction("");
	}


	void OnTriggerStay2D(Collider2D theCollider) {
		if(theCollider.gameObject.tag == "hero") {

		}
	}
}
