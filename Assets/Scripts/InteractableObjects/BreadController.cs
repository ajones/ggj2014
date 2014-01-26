	using UnityEngine;
using System.Collections;

public class BreadController : StickyObjectController {

	public override void itemGrabbed() {
		Debug.Log ("itemGrabbed");
		EventManager.TriggerEvent(new GameStateProgressEvent(GameManager.GameState.AquireBread));
	}
}
