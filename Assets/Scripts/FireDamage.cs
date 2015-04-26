using UnityEngine;
using System.Collections;

public class FireDamage : MonoBehaviour {

	public int fireDamage = 1;
	string playerPrefabName = "Player";

	void OnTriggerEnter2D(Collider2D other) {
    	if (other.gameObject.name == playerPrefabName) {
    		other.gameObject.GetComponent<PlayerController>().hp -= fireDamage;
    	}
    }

    void OnTriggerStay2D(Collider2D other) {
    	if (other.gameObject.name == playerPrefabName) {
    		other.gameObject.GetComponent<PlayerController>().hp -= fireDamage;
    	}
    }
}
