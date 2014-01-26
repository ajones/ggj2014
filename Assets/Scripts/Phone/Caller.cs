using UnityEngine;
using System.Collections;

[RequireComponent(typeof(tk2dSpriteAnimator))]
public class Caller : MonoBehaviour, IEventListener {

	private tk2dSpriteAnimator anim;

	public void HangUp() {
		anim.Play ("Idle");
	}

	void Call() {
		anim.Play ("Ring");
	}

	void Awake() {
		EventManager.AddListener (this, "PhoneCall");
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<tk2dSpriteAnimator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "PhoneCall":
			Call();
			break;
		}        
		return true;
	}
}
