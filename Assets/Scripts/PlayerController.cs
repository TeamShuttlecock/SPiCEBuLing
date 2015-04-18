using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator _anim;
	private Rigidbody2D _rb2d;
	private float _jumpInterval = 0;
	private GameObject waterTrail;

	// Use this for initialization
	void Start() {
		_anim = GetComponent<Animator>();
		_rb2d = GetComponent<Rigidbody2D>();
		waterTrail = GameObject.Find("WaterTrail");
	}

	void Update() {
		if (_jumpInterval > 0) {
			_jumpInterval -= Time.deltaTime;
		}
		// if (_jumpInterval <= 0.0f) {
			if (Input.GetKeyDown("space") || Input.GetKeyDown("w") || Input.GetKeyDown("up")) {
				_rb2d.AddForce(Vector2.up * 500);
				_jumpInterval = 2.0f;
			}
		// }
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
			Vector3 rot = waterTrail.transform.eulerAngles;
			waterTrail.transform.eulerAngles = new Vector3(rot.x, -90, rot.z);
			if (waterTrail.GetComponent<ParticleSystem>().isPlaying) {
				waterTrail.GetComponent<ParticleSystem>().Stop();
				// waterTrail.SetActive(false);
			}
			return;
		}

		if (Input.GetKey("right") || Input.GetKey("d"))
		{
			_anim.SetFloat("rightInput", 1);
			_rb2d.velocity = new Vector2(3, _rb2d.velocity.y);
			Vector3 rot = waterTrail.transform.eulerAngles;
			waterTrail.transform.eulerAngles = new Vector3(rot.x, 90, rot.z);
			if (waterTrail.GetComponent<ParticleSystem>().isPlaying) {
				waterTrail.GetComponent<ParticleSystem>().Stop();
				// waterTrail.SetActive(false);
			}
			return;
		}

		// if (!waterTrail.GetComponent<ParticleSystem>().isPlaying) {
			// waterTrail.SetActive(true);
			waterTrail.GetComponent<ParticleSystem>().Play();
		// }
	}
}
