using UnityEngine;
using System.Collections;

public class CameraShakeEvent : IEvent {
	public CameraShakeEvent() {}    
	string IEvent.GetName() {
		return "CameraShakeEvent";
	}
	object IEvent.GetData() {
		return null;
	}
}
