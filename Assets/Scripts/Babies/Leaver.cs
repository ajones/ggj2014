using UnityEngine;
using System.Collections;


[RequireComponent(typeof(tk2dSprite))]
public class Leaver : MonoBehaviour {

	public float speed;
	
	private tk2dSprite sprite;

	void Awake() {
		sprite = GetComponent<tk2dSprite> ();
	}

	public void Leave() {
		sprite.FlipX = true;
		StartCoroutine ("ReallyLeave");
	}

	IEnumerator ReallyLeave() {
		while (true) {
			transform.Translate(speed * Time.deltaTime, 0, 0);
			yield return null;
		}
	}
}
