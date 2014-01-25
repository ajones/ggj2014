using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public float fadeTime;
	private Material material;
	private Color newColor;

	// Use this for initialization
	void Start () {
		material = renderer.material;
		newColor = new Color (material.color.r, material.color.g, material.color.b, material.color.a);
	}
	
	// Update is called once per frame
	void Update () {
		newColor.a -= fadeTime * Time.deltaTime;
		material.color = newColor;
	}
}
