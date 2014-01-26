using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour, IEventListener {

	public GameObject phone;
	public Vector3 origin;
	public Vector2 force;
	public float phoneCallDelay;

	

	void Awake() {
		EventManager.AddListener (this, "PhoneThrow");
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Throw() {
		GameObject newPhone = Instantiate (phone, origin, phone.transform.rotation) as GameObject;
		Rigidbody2D rBody = newPhone.GetComponent<Rigidbody2D> ();
		rBody.AddForce (force);
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "PhoneThrow":
			Throw();
			EventManager.TriggerEventAfter(new PhoneCall(), phoneCallDelay);
			break;
		}        
		return true;
    }
}
