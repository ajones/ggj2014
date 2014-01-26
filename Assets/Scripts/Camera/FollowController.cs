using UnityEngine;
using System.Collections;

public class FollowController : MonoBehaviour {

	public GameObject target;
	public float xGive;
	public float yGive;
	public Vector2 minPoint;
	public Vector2 maxPoint;

	// variables for caching
	private Camera cam;
	private Transform camTransform;
	private Transform targetTransform;
	private Vector3 newCamPosition;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
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
	
}
