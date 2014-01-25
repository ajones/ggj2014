using UnityEngine;
using System.Collections;

public class ZoomController : MonoBehaviour, IEventListener {

	// we may want to have this be data associated with the event, but right now this works
	public float inSize;
	public float outSize;
	public float moveSpeed;

	private Camera cam;

	void Awake() {
		EventManager.AddListener(this, "LevelStart");
		EventManager.AddListener(this, "Segway");
	}

	// Use this for initialization
	void Start() {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "LevelStart":
			StartCoroutine("ZoomIn");
			break;
		case "Segway":
			StartCoroutine("ZoomOut");
			break;
		default:
			Debug.Log ("Default!");
			break;
		}        
		return false;
	}

	IEnumerator ZoomIn() {
		float originalSize = cam.orthographicSize;
		float diffInSize = originalSize - inSize;
		float currentDiff = 0;
		while (cam.orthographicSize > inSize) {
			currentDiff = Mathf.Lerp (currentDiff, diffInSize, moveSpeed * Time.deltaTime);
			cam.orthographicSize = originalSize - currentDiff;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator ZoomOut() {
		while (cam.orthographicSize < outSize) {
			cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, outSize, moveSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
}
