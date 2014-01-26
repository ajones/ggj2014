using UnityEngine;
using System.Collections;

public class SwingOpener : MonoBehaviour, IEventListener {

	public float speed;
	public float phoneThrowDelay;

	private Transform myTransform;
	private Quaternion newRotation;

	void Awake() {
		EventManager.AddListener(this, "DoorOpen");
	}

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SwingOpen() {
		while (myTransform.rotation.y > 0) {
			float newY = myTransform.rotation.y - speed * Time.deltaTime;
			newRotation = new Quaternion(myTransform.rotation.x, newY, myTransform.rotation.z, myTransform.rotation.w);
			myTransform.rotation = newRotation;
			yield return new WaitForEndOfFrame();
		}
		newRotation = new Quaternion (myTransform.rotation.x, 0, myTransform.rotation.z, myTransform.rotation.w);
		EventManager.TriggerEventAfter (new PhoneThrow (), phoneThrowDelay);
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "DoorOpen":
			StartCoroutine("SwingOpen");
			break;
		}        
		return false;
	}
}
