using UnityEngine;
using System.Collections;

public class StickyObjectController : MonoBehaviour {
	GameObject myHero;

	void OnCollisionEnter2D(Collision2D collision ) {
		Debug.Log ("this is running1");
		if(collision.gameObject.tag=="hero"){ 
			collision.gameObject.SendMessage("itemCaptured", this.gameObject);
			myHero = collision.gameObject;
			this.itemGrabbed();
		}
		if (myHero && collision.gameObject.tag == "rightWall") {
			myHero.GetComponent<HeroAnimationController>().isOnRightWall = true;
			myHero.GetComponent<HeroAnimationController>().isOnLeftWall = false;
		} else if (myHero && collision.gameObject.tag == "leftWall") {
			myHero.GetComponent<HeroAnimationController>().isOnRightWall = false;
			myHero.GetComponent<HeroAnimationController>().isOnLeftWall = true;
		}
	}

	public virtual void itemGrabbed() {
	}

	void OnCollisionExit2D(Collision2D collision ) {
		Debug.Log ("this is running2");
		if (myHero && (collision.gameObject.tag == "rightWall" || collision.gameObject.tag == "leftWall")) {
			Debug.Log ("left wall with object!");
			myHero.GetComponent<HeroAnimationController>().isOnRightWall = false;
			myHero.GetComponent<HeroAnimationController>().isOnLeftWall = false;
		} 
	}

	void OnTriggerExit2D(Collider2D collider ) {
		if(collider.gameObject.tag=="hero"){ 
			this.collider2D.isTrigger = false;
			this.gameObject.AddComponent<Rigidbody2D>();
			this.itemDropped();
		}

	}

	public virtual void itemDropped() {
	}

}

