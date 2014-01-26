using UnityEngine;
using System.Collections;

public class SceneReloader : MonoBehaviour {

	AsyncOperation loadInBackground;
	public string levelName = "movementTest";
	private bool loading = false;

	// Use this for initialization
	void Start () {
	}


	public void ResetLevel() {
		loading = true;
		StartCoroutine ("Reload");
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator Reload() {
		if (loadInBackground == null) {
		    loadInBackground = Application.LoadLevelAsync (levelName);
		}
		while (!loadInBackground.isDone) {
			yield return null;
		}
		loading = false;
		Application.LoadLevel (levelName);
	}
}
