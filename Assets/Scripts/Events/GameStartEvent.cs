using UnityEngine;
using System.Collections;

public class GameStartEvent : IEvent {
	public GameStartEvent() {}    
	string IEvent.GetName() {
		return "GameStartEvent";
	}
	object IEvent.GetData() {
		return null;
	}
}