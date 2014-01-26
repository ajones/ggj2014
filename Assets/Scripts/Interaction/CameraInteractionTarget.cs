using UnityEngine;
using System.Collections;

public class CameraInteractionTarget : InteractionTarget {

	public FadeController fadeController;

	public override void beginInteraction (string id) {
		
		EventManager.TriggerEvent(new LevelStart());
		fadeController.FadeOut();
	}
	
	public override void stopInteraction (string id) {
		EventManager.TriggerEvent(new Segway());
		fadeController.FadeIn();
	}
	
	
	public override void heroInteracted() {
	}
}
