using UnityEngine;
using System.Collections;

public class DoorOpen : IEvent {

	public DoorOpen() {}

	string IEvent.GetName() {
		return "DoorOpen";
	}

	object IEvent.GetData() {
		return null;
	}
}
