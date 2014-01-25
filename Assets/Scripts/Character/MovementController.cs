using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	float movementSpeed = 5000f;
	float jumpSpeed = 100000f;

	GameObject capturedItem = null;
	GameObject popUp = null;

	tk2dSprite sprite;

	void Awake() {
		this.sprite = this.gameObject.GetComponent<tk2dSprite>();
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up") || Input.GetKey ("w")){
			this.destroyPopupIfNecessary();
			this.rigidbody2D.AddForce(new Vector3(0,jumpSpeed,0));
		}
		if (Input.GetKey ("right") || Input.GetKey ("d")){
			this.destroyPopupIfNecessary();
			this.rigidbody2D.AddForce(new Vector3(movementSpeed,0,0));

			this.sprite.FlipX = false;
		}
		if (Input.GetKey ("left") || Input.GetKey ("a")){
			this.destroyPopupIfNecessary();
			this.rigidbody2D.AddForce(new Vector3(movementSpeed * -1,0,0));
			this.sprite.FlipX = true;
		}
		if (Input.GetKeyDown ("space")){
			if (this.capturedItem != null){
				this.capturedItem.transform.parent = null;
				this.capturedItem.collider2D.isTrigger = true;
				this.capturedItem = null;
			}

			this.destroyPopupIfNecessary();
			this.popUp = PopupManager.getInstance().showPopupFromGameObject("testPopup",this.gameObject, new Vector3(0,0.75f,0));
		}
	}

	void destroyPopupIfNecessary() {
		if (this.popUp != null){
			GameObject.Destroy(this.popUp);
		}
	}


	void itemCaptured (GameObject item) {
		this.capturedItem = item;
		GameObject.Destroy(this.capturedItem.rigidbody2D);

	}
}
