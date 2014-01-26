using UnityEngine;
using System.Collections;

public class Caller : MonoBehaviour, IEventListener {

	public tk2dSpriteAnimator anim;
	public GameObject phoneSprite;

	public float shakeSize;
	public float shakeStep;

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
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	IEnumerator ShakePhone() {
		Vector3 startingPos = phoneSprite.transform.localPosition;
	
		while (anim.IsPlaying("Ring")) {
			phoneSprite.transform.localPosition = new Vector3 (
				startingPos.x + ((Random.value * shakeSize) - shakeSize / 2f),
				startingPos.y,
				startingPos.z 
				);
		
			yield return new WaitForSeconds (shakeStep);
		}
	
		phoneSprite.transform.localPosition = startingPos;
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "PhoneCall":
			Call ();
			StartCoroutine("ShakePhone");
			break;
		}        
		return true;
	}
}
