using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	float movementSpeed = 500f;
	float jumpSpeed = 10000f;

	GameObject capturedItem = null;
	Rigidbody capturedRigidBodyCache = null;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up")){
			this.rigidbody.AddForce(new Vector3(0,jumpSpeed,0));
		}
		if (Input.GetKey ("right")){
			this.rigidbody.AddForce(new Vector3(movementSpeed,0,0));
			this.transform.localScale = new Vector3(
				Mathf.Abs (this.transform.localScale.x),
				this.transform.localScale.y,
				this.transform.localScale.z);
		}
		if (Input.GetKey ("left")){
			this.rigidbody.AddForce(new Vector3(movementSpeed * -1,0,0));
			this.transform.localScale = new Vector3(
				Mathf.Abs (this.transform.localScale.x) * -1,
				this.transform.localScale.y,
				this.transform.localScale.z);
		}
		if (Input.GetKeyDown ("space")){
			if (this.capturedItem != null){
				print ("ahhh");
				this.capturedItem.transform.parent = null;
				this.capturedItem.collider.isTrigger = true;

				this.capturedItem = null;
			}
		}
	}


	void itemCaptured (GameObject item) {
		this.capturedItem = item;
		GameObject.Destroy(this.capturedItem.rigidbody);

	}
}
