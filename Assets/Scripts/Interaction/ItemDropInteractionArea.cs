using UnityEngine;
using System.Collections;

public class ItemDropInteractionArea : InteractionArea {

	public override void beginInteraction() {
		this.interactionTarget.beginInteraction("item-drop");
	}
	
	public override void stopInteraction() {
		this.interactionTarget.stopInteraction("item-drop");
	}
}
