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
		}
	}
	
	public override void stopInteraction (string id) {
		
	}
	
	
	public override void heroInteracted() {
	}



}
