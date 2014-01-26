using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour, IEventListener {

	public GameObject phoneShaker;
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
		Debug.Log ("throwing!");
		GameObject newPhone = Instantiate (phoneShaker, origin, phoneShaker.transform.rotation) as GameObject;
		newPhone.name = "phone";
		Rigidbody2D rBody = newPhone.GetComponentInChildren<Rigidbody2D> ();
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
