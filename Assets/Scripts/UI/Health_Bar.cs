using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
public class Health_Bar : MonoBehaviour {
	public float Get_Value ;
	public Image Bar_Image;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Mathf.Clamp01 (Get_Value);
		Bar_Image.fillAmount = Get_Value;
	}
}
