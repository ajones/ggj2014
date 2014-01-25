using UnityEngine;
using System.Collections;

public class ExampleEvent : IEvent {
	public ExampleEvent() {}    
    string IEvent.GetName() {
        return this.GetType().ToString();
    }
    object IEvent.GetData() {
        return null;
    }
}
