using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {

	int _level = 1;
	List<GameObject> _ghostFireList = new List<GameObject>();
	GameObject _ghostFirePrefab;
	GameObject _platformSpawner;
	GameObject _player;

	// Use this for initialization
	void Start () {
		_ghostFirePrefab = (GameObject)Resources.Load("Prefabs/GhostFire");
		_platformSpawner = GameObject.Find("PlatformSpawner");
		spawnGhostFire();
		_player = GameObject.Find("Player");
		if (_player == null) {
			Debug.Log("Player not found!");
		}
	}

	void spawnGhostFire() {
		for (int i = 0; i < (int)Mathf.Sqrt(_level); i++) {
			GameObject ghostFire = (GameObject)Object.Instantiate(_ghostFirePrefab);
			ghostFire.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(-3, 3), 0);
			_ghostFireList.Add(ghostFire);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_ghostFireList.Count == 0) {
			_level++;
			spawnGhostFire();
			_platformSpawner.GetComponent<PlatformSpawner>().Respawn();
			// Recover player's hp
			_player.GetComponent<PlayerController>().Recover();
		}
		// Game end
		if (_player.GetComponent<PlayerController>().hp <= 0) {
			//Just restart the game now
			Restart();
		}
	}

	void Restart() {
		//Clear all the current fires
		foreach(GameObject fire in _ghostFireList) {
			Destroy(fire);
		}
		_ghostFireList.Clear();
		_level = 1;
		spawnGhostFire();
		_platformSpawner.GetComponent<PlatformSpawner>().Respawn();
		// Recover player's hp
		_player.GetComponent<PlayerController>().Restart();
	}

	public void DestroyGhostFire(GameObject fire) {
		for (int i = _ghostFireList.Count - 1; i >= 0; i--) {
			if (_ghostFireList[i] == fire) {
				_ghostFireList.RemoveAt(i);
				Destroy(fire);
			}
		}
	}

	// A very basic UI
	void OnGUI() {
        GUI.enabled = true;
        string text;
        GUILayout.BeginArea(new Rect(15, 15, 150, 150));
        text = "Player HP : " + _player.GetComponent<PlayerController>().hp;
        GUILayout.TextArea(text);
        GUILayout.Space(20);
        text = "Level : " + _level;
        GUILayout.TextArea(text);
        GUILayout.EndArea();
    }
}
