using UnityEngine;
using System.Collections;

public class FollowController : MonoBehaviour {

	public GameObject target;

	// variables for caching
	private Transform myTransform;
	private Transform targetTransform;


	// Use this for initialization
	void Start () {
		myTransform = transform;
		targetTransform = target.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3 (targetTransform.position.x, targetTransform.position.y, myTransform.position.z);
		myTransform.position = newPosition;
	}
}
