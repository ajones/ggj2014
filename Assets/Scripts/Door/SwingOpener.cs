using UnityEngine;
using System.Collections;

public class SwingOpener : MonoBehaviour, IEventListener {

	public float speed;

	private Transform myTransform;
	private Quaternion newRotation;

	void Awake() {
		EventManager.AddListener(this, "PhoneThrow");
	}

	// Use this for initialization
	void Start () {
		myTransform = transform;
		EventManager.TriggerEventAfter (new PhoneThrow (), 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SwingOpen() {
		while (myTransform.rotation.y > 0) {
			Debug.Log ("SWINING");
			float newY = myTransform.rotation.y - speed * Time.deltaTime;
			newRotation = new Quaternion(myTransform.rotation.x, newY, myTransform.rotation.z, myTransform.rotation.w);
			myTransform.rotation = newRotation;
			yield return new WaitForEndOfFrame();
		}
		newRotation = new Quaternion (myTransform.rotation.x, 0, myTransform.rotation.z, myTransform.rotation.w);
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "PhoneThrow":
			Debug.Log ("PHONE THROW!");
			StartCoroutine("SwingOpen");
			break;
		default:
			break;
		}        
		return false;
	}
}
