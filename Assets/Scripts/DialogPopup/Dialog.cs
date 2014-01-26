using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

	public TextMesh messageMesh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMessage (string message) {
		messageMesh.text = message;
	}

}
