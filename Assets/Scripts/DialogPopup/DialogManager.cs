using UnityEngine;
using System.Collections;

public class DialogManager : Manager {

	Dialog currentlyVisibleDialog = null;
	public GameObject dialogAnchor;

	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;
	
	public static DialogManager getInstance() 
	{
		return getInstance(ref instance, "DialogManager") as DialogManager;
	}
	
	
	void Awake() {

	}



	public void ShowDialog(string dialogName, string message) {
		this.DismissDialog();

		GameObject prefab = Resources.Load ("Prefabs/Dialogs/"+dialogName) as GameObject;
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		go.transform.parent = dialogAnchor.transform;
		go.transform.localPosition = Vector3.zero;
		currentlyVisibleDialog = go.GetComponent<Dialog>();
		currentlyVisibleDialog.SetMessage(message);

	}

	public void DismissDialog() {
		if (currentlyVisibleDialog != null){
			GameObject.Destroy(currentlyVisibleDialog.gameObject);
		}
	}

	

}
