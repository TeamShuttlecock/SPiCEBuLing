using UnityEngine;
using System.Collections;

public class GhostFire : MonoBehaviour {

	int _hp = 100;
	GameObject _levelManager;

	// Use this for initialization
	void Start () {
		_levelManager = GameObject.Find("LevelManager");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
        //Destroy(other.gameObject);
        Debug.Log(other);
    }

    void OnParticleCollision(GameObject other) {
        // Rigidbody body = other.GetComponent<Rigidbody>();
        // if (body) {
        //     Vector3 direction = other.transform.position - transform.position;
        //     direction = direction.normalized;
        //     body.AddForce(direction * 5);
        // }
        _hp--;
        if (_hp <= 0) {
        	_levelManager.GetComponent<LevelManager>().DestroyGhostFire(gameObject);
        }
    }
}
