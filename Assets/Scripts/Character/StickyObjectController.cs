using UnityEngine;
using System.Collections;

public class StickyObjectController : MonoBehaviour {
	

	void OnCollisionEnter(Collision collision ) {
		if(collision.gameObject.tag=="hero"){ 
			print("hit");
			this.transform.parent = collision.gameObject.transform;
			this.transform.localPosition = new Vector3(0.31f,0.41f,-1);
			collision.gameObject.SendMessage("itemCaptured", this.gameObject);
		}
	}

	void OnTriggerExit(Collider collider ) {
		if(collider.gameObject.tag=="hero"){ 
			this.collider.isTrigger = false;
			Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
		}
	}

}

