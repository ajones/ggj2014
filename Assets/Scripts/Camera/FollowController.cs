using UnityEngine;
using System.Collections;

public class FollowController : MonoBehaviour,IEventListener {
	
	public GameObject target;
	public GameObject startTarget;
	public float xGive;
	public float yGive;
	public Vector2 minPoint;
	public Vector2 maxPoint;

	// variables for caching
	private Camera cam;
	private Transform camTransform;
	private Transform targetTransform;
	private Vector3 newCamPosition;

	bool shouldFollow = false;

	// Use this for initialization
	void Start () {
		EventManager.AddListener(this,"GameStartEvent");
		cam = Camera.main;
		camTransform = Camera.main.transform;
		targetTransform = startTarget.transform;
		newCamPosition = new Vector3(targetTransform.position.x, targetTransform.position.y, camTransform.position.z);
		camTransform.position = newCamPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (!shouldFollow) return;

		float xDiff;
		float yDiff;

		if (targetTransform.position.x < camTransform.position.x - xGive) {
			xDiff = (camTransform.position.x - xGive) - targetTransform.position.x; 
			newCamPosition.x -= xDiff;
			camTransform.position = newCamPosition;
			// Move our position a step closer to the target.
		} else if (targetTransform.position.x > camTransform.position.x + xGive) {
			xDiff = targetTransform.position.x - (camTransform.position.x + xGive);
			newCamPosition.x += xDiff;
			camTransform.position = newCamPosition;
			// Move our position a step closer to the target
		}

		if (targetTransform.position.y < camTransform.position.y - yGive) {
			yDiff = (camTransform.position.y - yGive) - targetTransform.position.y;
			newCamPosition.y -= yDiff;
			camTransform.position = newCamPosition;
			// Move our position a step closer to the target.
			//camTransform.position = Vector3.Slerp(camTransform.position, newCamPosition, Time.deltaTime);
		} else if (targetTransform.position.y > camTransform.position.y + yGive) {
			yDiff = targetTransform.position.y - (camTransform.position.y + yGive);
			newCamPosition.y += yDiff;
			camTransform.position = newCamPosition;
			// Move our position a step closer to the target.
			//camTransform.position = Vector3.Slerp(camTransform.position, newCamPosition, Time.deltaTime);
    	}
			


		// Lock the cam's position
		float height = cam.orthographicSize * 2f;
		float width = cam.orthographicSize * cam.aspect;
		float x0 = camTransform.position.x - width / 2f;
		float x1 = camTransform.position.x + width / 2f;
		float y0 = camTransform.position.y - height / 2f;
		float y1 = camTransform.position.y + height / 2f;

		if (x0 < minPoint.x) {
			newCamPosition.x = minPoint.x + width / 2f;;
			camTransform.position = newCamPosition;
		} else if (x1 > maxPoint.x) {
			newCamPosition.x = maxPoint.x - width / 2f;
			camTransform.position = newCamPosition;
		}
		
		if (y0 < minPoint.y) {
			newCamPosition.y = minPoint.y + height / 2f;
			camTransform.position = newCamPosition;
		} else if (y1 > maxPoint.y) {
			newCamPosition.y = maxPoint.y - height / 2f;
			camTransform.position = newCamPosition;
		}
		
	}

	IEnumerator lerpToHero () {
		float accum = 0;
		Vector3 start = camTransform.position;

		while(accum < 1){
			camTransform.position = Vector3.Slerp(
				start,
				new Vector3(
					target.transform.position.x,
					target.transform.position.y,
					camTransform.position.z	),
				accum);
			accum += Time.deltaTime /3f;
			yield return new WaitForEndOfFrame();
		}
		this.shouldFollow = true;
		camTransform.position = new Vector3(
			target.transform.position.x,
			target.transform.position.y,
			camTransform.position.z	);
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "GameStartEvent":
			targetTransform = target.transform;
			this.shouldFollow = false;
			StartCoroutine(lerpToHero());
			break;
		}        
		return false;
	}
	
}
