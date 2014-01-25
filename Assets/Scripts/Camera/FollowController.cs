using UnityEngine;
using System.Collections;

public class FollowController : MonoBehaviour {

	public GameObject target;
	public float xGive;
	public float yGive;

	// variables for caching
	private Transform camTransform;
	private Transform targetTransform;
	private Vector3 newCamPosition;

	// Use this for initialization
	void Start () {
		camTransform = Camera.main.transform;
		targetTransform = target.transform;
		newCamPosition = new Vector3(targetTransform.position.x, targetTransform.position.y, camTransform.position.z);
		camTransform.position = newCamPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float xDiff;
		float yDiff;

		if (targetTransform.position.x < camTransform.position.x - xGive) {
			xDiff = (camTransform.position.x - xGive) - targetTransform.position.x; 
			newCamPosition.x -= xDiff;
			camTransform.position = newCamPosition;
		} else if (targetTransform.position.x > camTransform.position.x + xGive) {
			xDiff = targetTransform.position.x - (camTransform.position.x + xGive);
			newCamPosition.x += xDiff;
			camTransform.position = newCamPosition;
		}

		if (targetTransform.position.y < camTransform.position.y - yGive) {
			yDiff = (camTransform.position.y - yGive) - targetTransform.position.y;
			newCamPosition.y -= yDiff;
			camTransform.position = newCamPosition;
		} else if (targetTransform.position.y > camTransform.position.y + yGive) {
			yDiff = targetTransform.position.y - (camTransform.position.y + yGive);
			newCamPosition.y += yDiff;
			camTransform.position = newCamPosition;
    	}
		
	}
	
}
