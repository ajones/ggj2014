using UnityEngine;
using System.Collections;

public class LevelStart : IEvent {
	public LevelStart() {}    
	string IEvent.GetName() {
		return "LevelStart";
	}
	object IEvent.GetData() {
		return null;
	}
}

