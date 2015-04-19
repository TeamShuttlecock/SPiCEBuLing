using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {

	int _level = 1;
	List<GameObject> _ghostFireList = new List<GameObject>();
	GameObject _ghostFirePrefab;
	GameObject _platformSpawner;

	// Use this for initialization
	void Start () {
		_ghostFirePrefab = (GameObject)Resources.Load("Prefabs/GhostFire");
		_platformSpawner = GameObject.Find("PlatformSpawner");
		spawnGhostFire();
	}

	void spawnGhostFire() {
		for (int i = 0; i < (int)Mathf.Sqrt(_level); i++) {
			GameObject ghostFire = (GameObject)Object.Instantiate(_ghostFirePrefab);
			ghostFire.transform.position = new Vector3((int)Random.Range(-7, 7), (int)Random.Range(-3, 3), 0);
			_ghostFireList.Add(ghostFire);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_ghostFireList.Count == 0) {
			_level++;
			Debug.Log("Level : " + _level);
			spawnGhostFire();
			_platformSpawner.GetComponent<PlatformSpawner>().Respawn();
		}	
	}

	public void DestroyGhostFire(GameObject fire) {
		for (int i = _ghostFireList.Count - 1; i >= 0; i--) {
			if (_ghostFireList[i] == fire) {
				_ghostFireList.RemoveAt(i);
				Destroy(fire);
			}
		}
	}
}
