using UnityEngine;
using System.Collections;

public class Stretcher : MonoBehaviour {
	public Vector3 stretchTime;
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
