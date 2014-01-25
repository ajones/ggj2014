using UnityEngine;
using System.Collections;

/// <summary>
/// Stretcher or scales an entity by a specified amount.
/// </summary>
public class Stretcher : MonoBehaviour {

	public Vector3 stretchTime; // the speed at which the entity is stretched on each axis
	
	private Transform myTransform;
	private Vector3 newScale;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		newScale = myTransform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		newScale = newScale + stretchTime * Time.deltaTime;
		myTransform.localScale = newScale;
	}
}
