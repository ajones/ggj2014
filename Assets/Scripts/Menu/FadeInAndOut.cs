using UnityEngine;
using System.Collections;

public class FadeInAndOut : MonoBehaviour {

	tk2dSprite sprite;
	// Use this for initialization
	void Start () {
		this.sprite = this.GetComponent<tk2dSprite>();
		StartCoroutine(Fade());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Fade() {
		float fadeSpeed = 1f;
		float accum = 0f;
		float maxAlpha = 0.25f;
		while (true){
			accum = 0f;
			while (accum < 1f){
				this.sprite.color = new Color(this.sprite.color.r,this.sprite.color.g,this.sprite.color.b,Mathf.Lerp(0,maxAlpha,accum));
				accum += Time.deltaTime * fadeSpeed;
				yield return new WaitForEndOfFrame();
			}
			accum = 0f;
			while (accum < 1f){
				this.sprite.color = new Color(this.sprite.color.r,this.sprite.color.g,this.sprite.color.b,Mathf.Lerp(maxAlpha,0,accum));
				accum += Time.deltaTime * fadeSpeed;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}

