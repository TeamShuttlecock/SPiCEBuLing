using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator _anim;

	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		_anim.SetFloat("leftInput", 0);
		_anim.SetFloat("rightInput", 0);

		if (Input.GetKey("left") || Input.GetKey("a"))
		{
			_anim.SetFloat("leftInput", 1);
		}

		if (Input.GetKey("right") || Input.GetKey("d"))
		{
			_anim.SetFloat("rightInput", 1);
		}

	}
}
