using UnityEngine;
using System.Collections;

public class FrontDoorInteractionArea : InteractionArea, IEventListener {

	void Awake() {
		EventManager.AddListener(this,"DoorOpen");
	}
	void OnDestroy() {
		EventManager.DetachListener(this,"DoorOpen");
	}

	public override void beginInteraction() {
		this.interactionTarget.beginInteraction("front-door");
	}
	
	public override void stopInteraction() {
		this.interactionTarget.stopInteraction("front-door");
	}


	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "DoorOpen":
			GameObject.Destroy(this.gameObject);
			break;
		}        
		return false;
	}
}
