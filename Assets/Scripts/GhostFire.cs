using UnityEngine;
using System.Collections;

public class GhostFire : MonoBehaviour {

	int _hp = 100;
	GameObject _levelManager;
	ParticleSystem _smoking;
	bool _beHitting = false;
	float _hittingTime = 0.0f;
	GameObject _extinguishedSmokePrefab;

	// Use this for initialization
	void Start () {
		_extinguishedSmokePrefab = (GameObject)Resources.Load("Prefabs/ExtinguishedSmoke");
		_levelManager = GameObject.Find("LevelManager");
		foreach (ParticleSystem smoking in GetComponentsInChildren<ParticleSystem>()) {
			Debug.Log(smoking.gameObject.name);
			if (smoking.gameObject.name == "Smoking") {
				_smoking = smoking;
				_smoking.Stop();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_beHitting) {
			_hittingTime -= Time.deltaTime;
			if (_hittingTime < 0.0f) {
				_beHitting = false;
				_smoking.Stop();
			}
		}
	}

    void OnParticleCollision(GameObject other) {
        _hp--;
        _hittingTime = 0.3f;
        _beHitting = true;
        _smoking.Play();
        if (_hp <= 0) {
        	_levelManager.GetComponent<LevelManager>().DestroyGhostFire(gameObject);
        	GameObject extinguishedSmoke = (GameObject)Object.Instantiate(_extinguishedSmokePrefab);
        	extinguishedSmoke.transform.position = gameObject.transform.position;
        }
    }
}
