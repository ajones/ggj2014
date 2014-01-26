using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

	public List<TextMesh> meshes;
	public tk2dSprite bg;



	// Use this for initialization
	void Update () {
		if (Input.GetKeyDown ("space")){
			StartCoroutine(FadeOut());
		}
	}
	
	IEnumerator FadeOut() {
		float accum = 0f;
		float bgStartA = bg.color.a;

		while (accum < 1f) {
			foreach(TextMesh tm in meshes){
				tm.color = new Color(tm.color.r,tm.color.g,tm.color.b,Mathf.Lerp(1f,0f,accum)); 
				accum += Time.deltaTime;
				yield return new WaitForEndOfFrame();	
			}
			bg.color = new Color(bg.color.r,bg.color.g,bg.color.b,Mathf.Lerp(bgStartA,0f,accum)); 

			accum += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		foreach(TextMesh tm in meshes){
			tm.color = new Color(tm.color.r,tm.color.g,tm.color.b,0); 
			accum += Time.deltaTime;
			yield return new WaitForEndOfFrame();	
		}
		bg.color = new Color(bg.color.r,bg.color.g,bg.color.b,0); 


		GameObject.Destroy(this.gameObject);
	}
}
