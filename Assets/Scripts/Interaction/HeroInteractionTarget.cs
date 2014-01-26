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
				StartCoroutine(WaitThenEnableInteraction());

				ConversationManager.getInstance().PlayConversation1();
			}
			break;
		case "item-drop":
			if (GameManager.getInstance().getGameState() == GameManager.GameState.ItemDrop){
				this.heroController.DropItem();
			}
			break;
		}
	}
	
	public override void stopInteraction (string id) {
		
	}
	
	
	public override void heroInteracted() {
	}


	IEnumerator WaitThenEnableInteraction() {
		yield return new WaitForSeconds(1f);
		this.heroController.SetMovementInteractionEnabled(true);
		EventManager.TriggerEvent(new GameStateProgressEvent(GameManager.GameState.ApproachDoor));

	}



}
