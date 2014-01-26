using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour, IEventListener {

	public GameObject capturedItem = null;
	GameObject popUp = null;
	MovementController movementController;

	void Awake () {
		EventManager.AddListener(this,"GameOverEvent");
		EventManager.AddListener(this,"GameStartEvent");
		this.movementController = this.GetComponent<MovementController>();
	}


	void itemCaptured (GameObject item) {
		if (this.capturedItem != null) return;
		item.transform.parent = this.transform;
		this.capturedItem = item;
		GameObject.Destroy(this.capturedItem.rigidbody2D);
		this.movementController.SetUpCapturedItem();
	}

	void Update () {
		if (Input.GetKeyDown ("space")){
			if (this.capturedItem != null){
				this.capturedItem.transform.parent = null;
				this.capturedItem.collider2D.isTrigger = true;
				this.capturedItem = null;
			}
			
			this.destroyPopupIfNecessary();
			this.popUp = PopupManager.getInstance().showPopupFromGameObject("testPopup",this.gameObject, new Vector3(0,0.75f,0));
			
			this.quack();
			EventManager.TriggerEvent(new HeroInteractEvent());
			//EventManager.TriggerEvent(new CameraShakeEvent());
		}
	}

	public void destroyPopupIfNecessary() {
		if (this.popUp != null){
			GameObject.Destroy(this.popUp);
		}
	}

	
	void quack() {
		int quackIdx = (int)Mathf.Floor(Random.value * 4.0f);
		SoundManager.getInstance().playSoundEffect("quack" + quackIdx);
	}

	public void TryToFeedBabies() {
		if (this.capturedItem && this.capturedItem.gameObject.name == "bread"){
			GameObject.Destroy(this.capturedItem);
			this.capturedItem = null;
			EventManager.TriggerEvent(new GameStateProgressEvent(GameManager.GameState.FeedBabies));
			EventManager.TriggerEvent(new BabyFedEvent());
		}
	}

	public void SetMovementInteractionEnabled(bool enabled) {
		Debug.Log ("SetMovementInteractionEnabled" +enabled);
		this.movementController.interactionEnabled = enabled;
	}

	public void DropItem() {
		if (this.capturedItem){
			switch(this.capturedItem.gameObject.name){
			case "phone":
				ConversationManager.getInstance().PlayConversation2();
				break;
			case "flower":
				ConversationManager.getInstance().PlayConversation3();
				break;
			case "picture":
				ConversationManager.getInstance().PlayConversation4();
				break;
			case "book":
				ConversationManager.getInstance().PlayConversation5();
				break;
			}

			if (this.capturedItem.gameObject.name != "book"){
				GameManager.getInstance().breadSpawner.SpawnBread();
			}

			GameObject.Destroy(this.capturedItem);
			this.capturedItem = null;
			EventManager.TriggerEvent(new GameStateProgressEvent(GameManager.GameState.ItemDrop));
		}
	}


	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
		case "GameOverEvent":
			this.rigidbody2D.isKinematic = true;
			Leaver l = this.gameObject.AddComponent<Leaver>();
			l.speed = 15;
			l.Leave();
			SetMovementInteractionEnabled(false);
			break;
		case "GameStartEvent":
			SetMovementInteractionEnabled(true);
			break;
		}        
		return false;
	}
}

