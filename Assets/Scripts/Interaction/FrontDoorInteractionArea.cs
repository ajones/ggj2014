using UnityEngine;
using System.Collections;

public class FrontDoorInteractionArea : InteractionArea {

	public override void beginInteraction() {
		this.interactionTarget.beginInteraction("front-door");
	}
	
	public override void stopInteraction() {
		this.interactionTarget.stopInteraction("front-door");
	}
}
