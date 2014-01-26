using UnityEngine;
using System.Collections;

public class GameManager : Manager, IEventListener {

	public enum GameState {
		Intro,
		AquireBread,
		FeedBabies,
		ApproachDoor,
		AquirePhone,
		PlaceObject,
		AquireObject
	};


	public GameState gameState = GameState.Intro;


	GameState[] steps = new GameState[2]{
		GameState.FeedBabies,
		GameState.ApproachDoor
	};

	private int currentStep = 0;
	



	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;
	
	public static GameManager getInstance() 
	{
		return getInstance(ref instance, "GameManager") as GameManager;
	}
	

	void Awake() {
		EventManager.AddListener(this, "GameStateProgressEvent");
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

			switch(stateProgress){
				case GameState.FeedBabies :
					EventManager.TriggerEvent(new BabyFedEvent());
					break;
			}



			this.currentStep ++;
			Debug.Log("GAME STATE PROGRESS. now : " + this.steps[this.currentStep]);
		}
	}

}
