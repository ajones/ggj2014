using UnityEngine;
using System.Collections;

public class HeroInteractEvent : IEvent {
	public HeroInteractEvent() {}    
	string IEvent.GetName() {
		return "HeroInteractEvent";
	}
	object IEvent.GetData() {
		return null;
	}
}
