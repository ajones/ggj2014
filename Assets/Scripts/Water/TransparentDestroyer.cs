using UnityEngine;
using System.Collections;

public class TransparentDestroyer : MonoBehaviour {

	private Material material;

	// Use this for initialization
	void Start () {
		material = renderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		if (material.color.a <= 0) {
			Destroy(gameObject);
		}
	}
}
