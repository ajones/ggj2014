using UnityEngine;
using System.Collections;

public class PhoneCall : IEvent {

	public PhoneCall() {}

	string IEvent.GetName() {
		return "PhoneCall";
	}
	
	object IEvent.GetData() {
		return null;
	}
}
