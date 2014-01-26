using UnityEngine;
using System.Collections;

public class BabiesInteractionArea : InteractionArea {
	
	public override void beginInteraction() {
		Debug.Log ("beginInteraction");
		this.interactionTarget.beginInteraction("babies");
	}

	public override void stopInteraction() {
		this.interactionTarget.stopInteraction("babies");
	}

}
