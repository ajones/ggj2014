using UnityEngine;
using System.Collections;

public class GameManagerInteractionTarget : InteractionTarget {

	public override void beginInteraction (string id) {
		
		switch(id) {
			case "babies":
				EventManager.TriggerEvent(new GameStateProgressEvent(GameManager.GameState.FeedBabies));
				break;
		}
	}
	
	public override void stopInteraction (string id) {

	}
	
	
	public override void heroInteracted() {
	}
}
