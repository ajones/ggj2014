using UnityEngine;
using System.Collections;

public class BreadSpawner : MonoBehaviour {

	public void SpawnBread() {
		GameObject prefab = Resources.Load ("Prefabs/Items/bread") as GameObject;
		GameObject go = GameObject.Instantiate(prefab) as GameObject;
		go.name = "bread";

		go.transform.position = this.transform.position;
	}
}
