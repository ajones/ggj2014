using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public bool interactionEnabled = true;

	float movementSpeed = 5000f;
	float jumpSpeed = 100000f;
	
	tk2dSprite sprite;
	HeroController heroController;

	void Awake() {
		this.sprite = this.gameObject.GetComponent<tk2dSprite>();
		this.heroController = this.gameObject.GetComponent<HeroController>();
	}


	// Update is called once per frame
	void Update () {

		if (!interactionEnabled) return;

		if (Input.GetKeyDown ("up") || Input.GetKey ("w")){
			this.heroController.destroyPopupIfNecessary();
			this.rigidbody2D.AddForce(new Vector3(0,jumpSpeed,0));
		}
		if (Input.GetKey ("right") || Input.GetKey ("d")){
			this.heroController.destroyPopupIfNecessary();
			this.rigidbody2D.AddForce(new Vector3(movementSpeed,0,0));

			this.sprite.FlipX = false;
			this.flipItemPosition(this.sprite.FlipX);
		}
		if (Input.GetKey ("left") || Input.GetKey ("a")){
			this.heroController.destroyPopupIfNecessary();
			this.rigidbody2D.AddForce(new Vector3(movementSpeed * -1,0,0));
			this.sprite.FlipX = true;
			this.flipItemPosition(this.sprite.FlipX);
		}
	}


	void flipItemPosition(bool flip) {
		if (this.heroController.capturedItem != null){
			Vector3 pos = new Vector3(0.23f,0.25f,-1);
			if (flip){
				pos.x *= -1;
			}
			this.heroController.capturedItem.transform.localPosition = pos;
		}
	}

	public void SetUpCapturedItem() {
		this.flipItemPosition(this.sprite.FlipX);
	}
}
