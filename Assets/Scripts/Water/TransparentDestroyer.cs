using UnityEngine;
using System.Collections;

/// <summary>
/// Destroys any object with a material whose alpha trans is 0 or less.
/// </summary>
public class TransparentDestroyer : MonoBehaviour {

	private Color color;

	// Use this for initialization
	void Start () {
		color = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (color.a <= 0) {
			Destroy(gameObject);
		}
	}
}
