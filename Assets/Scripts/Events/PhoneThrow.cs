using UnityEngine;
using System.Collections;

public class PhoneThrow : IEvent {

	public PhoneThrow() {}

	string IEvent.GetName() {
		return "PhoneThrow";
	}

	object IEvent.GetData() {
		return null;
	}
}
