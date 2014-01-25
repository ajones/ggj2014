using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	float movementSpeed = 5000f;
	float jumpSpeed = 100000f;

	GameObject capturedItem = null;


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up") || Input.GetKey ("w")){
			this.rigidbody2D.AddForce(new Vector3(0,jumpSpeed,0));
		}
		if (Input.GetKey ("right") || Input.GetKey ("d")){
			this.rigidbody2D.AddForce(new Vector3(movementSpeed,0,0));
			this.transform.localScale = new Vector3(
				Mathf.Abs (this.transform.localScale.x),
				this.transform.localScale.y,
				this.transform.localScale.z);
		}
		if (Input.GetKey ("left") || Input.GetKey ("a")){
			this.rigidbody2D.AddForce(new Vector3(movementSpeed * -1,0,0));
			this.transform.localScale = new Vector3(
				Mathf.Abs (this.transform.localScale.x) * -1,
				this.transform.localScale.y,
				this.transform.localScale.z);
		}
		if (Input.GetKeyDown ("space")){
			if (this.capturedItem != null){
				this.capturedItem.transform.parent = null;
				this.capturedItem.collider2D.isTrigger = true;
				this.capturedItem = null;
			}
		}
	}


	void itemCaptured (GameObject item) {
		this.capturedItem = item;
		GameObject.Destroy(this.capturedItem.rigidbody2D);

	}
}
