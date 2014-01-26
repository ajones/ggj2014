using UnityEngine;
using System.Collections;

public class GameStateProgressEvent : IEvent {

	GameManager.GameState progress;
	public GameStateProgressEvent(GameManager.GameState stateProgress) {
		this.progress = stateProgress;
	}    
	public GameManager.GameState GetProgress() {
		return this.progress;
	}

	string IEvent.GetName() {
		return "GameStateProgressEvent";
	}
	object IEvent.GetData() {
		return null;
	}
}