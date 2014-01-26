using UnityEngine;
using System.Collections;

public class FadeToBlack : MonoBehaviour,IEventListener {

	tk2dSprite sprite;
	// Use this for initialization
	void Start () {
		this.sprite = this.GetComponent<tk2dSprite>();
		EventManager.AddListener(this,"GameOverEvent");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator Fade() {
		float fadeSpeed = 0.25f;
		float accum = 0f;
		while (accum < 1f){
			this.sprite.color = new Color(this.sprite.color.r,this.sprite.color.g,this.sprite.color.b,Mathf.Lerp(0,1,accum));
			accum += Time.deltaTime * fadeSpeed;
			yield return null;
		}
	}

	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "GameOverEvent":
			StartCoroutine(Fade());
			break;
		}        
		return false;
	}
}


