using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BabiesController : MonoBehaviour,IEventListener {
	public List<GameObject> babies;


	void Awake() {
		EventManager.AddListener(this,"BabyFedEvent");
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "BabyFedEvent":
			Debug.Log ("Baby Fed Event");
			this.AnimateBabyExit();
			break;
		}        
		return false;
	}

	void AnimateBabyExit() {
		GameObject baby = this.babies[0];

		Leaver babyLeaver = baby.GetComponent<Leaver> ();
		babyLeaver.Leave ();
		this.babies.RemoveAt (0);
	}

}
