using UnityEngine;
using System.Collections;

public class CameraInteractionTarget : InteractionTarget {

	public override void beginInteraction () {
		
		EventManager.TriggerEvent(new LevelStart());
	}
	
	public override void stopInteraction () {
		EventManager.TriggerEvent(new Segway());
	}
	
	
	public override void heroInteracted() {
	}
}
