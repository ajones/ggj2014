using UnityEngine;
using System.Collections;

public class StickyObjectController : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D collision ) {
		if(collision.gameObject.tag=="hero"){ 
			this.transform.parent = collision.gameObject.transform;
			this.transform.localPosition = new Vector3(0.31f,0.41f,-1);
			collision.gameObject.SendMessage("itemCaptured", this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D collider ) {
		if(collider.gameObject.tag=="hero"){ 
			this.collider2D.isTrigger = false;
			this.gameObject.AddComponent<Rigidbody2D>();
		}
	}

}

