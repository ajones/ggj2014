using UnityEngine;
using System.Collections;

public class GameOverEvent : IEvent {
	public GameOverEvent() {}    
	string IEvent.GetName() {
		return "GameOverEvent";
	}
	object IEvent.GetData() {
		return null;
	}
}