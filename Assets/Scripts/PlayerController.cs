using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator _anim;
	private Rigidbody2D _rb2d;

	// Use this for initialization
	void Start() {
		_anim = GetComponent<Animator>();
		_rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {

		_anim.SetFloat("leftInput", 0);
		_anim.SetFloat("rightInput", 0);
		_rb2d.velocity = new Vector2(0, _rb2d.velocity.y);

		if (Input.GetKey("left") || Input.GetKey("a"))
		{
			_anim.SetFloat("leftInput", 1);
			_rb2d.velocity = new Vector2(-3, _rb2d.velocity.y);
		}

		if (Input.GetKey("right") || Input.GetKey("d"))
		{
			_anim.SetFloat("rightInput", 1);
			_rb2d.velocity = new Vector2(3, _rb2d.velocity.y);
		}

	}
}
