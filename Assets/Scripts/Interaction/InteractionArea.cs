using UnityEngine;
using System.Collections;

public class InteractionArea : MonoBehaviour {

	public InteractionTarget interactionTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter2D(Collider2D theCollider){
		if(theCollider.gameObject.tag == "hero") {
			if (this.interactionTarget != null){
				this.interactionTarget.beginInteraction();
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D theCollider){ 
		if(theCollider.gameObject.tag == "hero") {
			this.interactionTarget.stopInteraction();
		}
	}
	
	void OnTriggerStay2D(Collider2D theCollider) {
		if(theCollider.gameObject.tag == "hero") {

		}
	}
}
