using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour {

	public tk2dSprite spriteToFade;


	public void FadeIn() {
		StopCoroutine("FadeTo");
		StartCoroutine("FadeTo",1.0f);
	}

	public void FadeOut() {
		StopCoroutine("FadeTo");
		StartCoroutine("FadeTo",0.0f);
	}

	IEnumerator FadeTo(float targetAlpha){
		float accum = 0f;

		Color startingColor = spriteToFade.color;
		while (accum < 1f) {
			spriteToFade.color = new Color(startingColor.r,startingColor.g,startingColor.b,Mathf.Lerp(startingColor.a,targetAlpha,accum));
			accum += Time.deltaTime;
			yield return new WaitForEndOfFrame();	
		}
	}
}
