using UnityEngine;
using System.Collections;

public class PopupManager : Manager {

	/////////////////////  BOOTSTRAP  /////////////////////
	
	private static Manager instance;
	
	public static PopupManager getInstance() 
	{
		return getInstance(ref instance, "PopupManager") as PopupManager;
	}

	public GameObject showPopupFromGameObject(string popupPrefabName, GameObject anchorObject, Vector3 offset){ 

		GameObject popupPrefab = Resources.Load("Prefabs/Popups/"+popupPrefabName) as GameObject;

		if (popupPrefab == null){
			Debug.LogError("Could not find popup prefab: "+ popupPrefabName);
			return null;
		}

		GameObject popup = GameObject.Instantiate(popupPrefab) as GameObject;

		popup.transform.parent = anchorObject.transform;
		popup.transform.localPosition = offset;
		return popup;
	}

}
