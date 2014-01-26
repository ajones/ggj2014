using UnityEngine;
using System.Collections;

public class HeroInteractionTarget : InteractionTarget {

	HeroController heroController;
	void Awake() {
		this.heroController = this.gameObject.GetComponent<HeroController>();
	}

	public override void beginInteraction (string id) {
		
		switch(id) {
		case "babies":
			this.heroController.TryToFeedBabies();
			break;
		
		case "front-door":
			if (GameManager.getInstance().getGameState() == GameManager.GameState.ApproachDoor){
				this.heroController.SetMovementInteractionEnabled(false);
				EventManager.TriggerEvent(new DoorOpen());
			}
			break;
		}
	}
	
	public override void stopInteraction (string id) {
		
	}
	
	
	public override void heroInteracted() {
	}



}
