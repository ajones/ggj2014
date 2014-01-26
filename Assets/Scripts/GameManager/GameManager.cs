using UnityEngine;
using System.Collections;

public class GameManager : Manager, IEventListener {

	public enum GameState {
		FeedBabies,
		ApproachDoor,
		ItemDrop,
		AquireBread,
		GameOver
	};


	GameState[] steps = new GameState[8]{
		GameState.FeedBabies,
		GameState.ApproachDoor,
		GameState.ItemDrop,
		GameState.ItemDrop,
		GameState.ItemDrop,
		GameState.ItemDrop,
		GameState.FeedBabies,
		GameState.GameOver
	};

	private int currentStep = 0;
	
	public BreadSpawner breadSpawner;


	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;
	
	public static GameManager getInstance() 
	{
		return getInstance(ref instance, "GameManager") as GameManager;
	}
	

	void Awake() {
		EventManager.AddListener(this, "GameStateProgressEvent");
		Screen.showCursor = false;
	}


	/////////////////////  BLAH  /////////////////////


	bool IEventListener.HandleEvent(IEvent evt) {
		switch (evt.GetName()) {
			case "GameStateProgressEvent":
				GameState prog = ((GameStateProgressEvent)evt).GetProgress();
				this.CheckProgress(prog);
				break;
		}        
		return false;
	}


	void CheckProgress(GameState stateProgress){
		if (this.steps[this.currentStep] == stateProgress){

			switch(this.currentStep){
			case 0:
				ConversationManager.getInstance().PlayConversation1();
				break;
			}

			this.currentStep ++;
			Debug.Log("GAME STATE PROGRESS. now : " + this.steps[this.currentStep]);

			CheckGameOver();
		}
	}

	void CheckGameOver() {
		if (this.steps[this.currentStep] == GameState.GameOver){
			EventManager.TriggerEvent(new GameOverEvent());
		}
	}

	public GameState getGameState() {
		return this.steps[this.currentStep];
	}

}
