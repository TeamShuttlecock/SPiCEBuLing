using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
	public Wave(Vector2 Scroll)
	{
		_offsetX = Scroll.x;
		_offsetY = Scroll.y;
	}
	public Vector2 scroll_offSet_Speed;
	public MeshRenderer Mesh_To_Scroll;

	private float _offsetX ;
	private float _offsetY ;
	private Vector2 _offset_OverTime;

	private Material _material_To_Scroll;

	public void Set_Material()
	{
		_material_To_Scroll = Mesh_To_Scroll.material;

	}

	public void Scroll_Texture()
	{
		if (Mathf.Abs(_offsetX) > 0.5f || Mathf.Abs(_offsetY )> 0.5f) {
			_offsetX = 0;
			_offsetY = 0;
            _offsetX += scroll_offSet_Speed.x * Time.deltaTime;
            _offsetY += scroll_offSet_Speed.y * Time.deltaTime;
			
		} else if (Mathf.Abs(_offsetX) <= 0.5f || Mathf.Abs(_offsetY ) <= 0.5f) {
			_offsetX += scroll_offSet_Speed.x * Time.deltaTime;
            _offsetY += scroll_offSet_Speed.y * Time.deltaTime;
		}
		_offset_OverTime.Set (_offsetX, _offsetY);
		_material_To_Scroll.SetTextureOffset ("_MainTex", _offset_OverTime);
	}
}


public class TextureScrolling : MonoBehaviour {

	public List<Wave> Mesh_To_Scroll_List;

	void Start () {

		for (int i =0; i<Mesh_To_Scroll_List.Count; i++) 
		{
			Mesh_To_Scroll_List[i].Set_Material();
		}
	}
	
	void Update () {
		for (int i =0; i<Mesh_To_Scroll_List.Count; i++) 
		{
			Mesh_To_Scroll_List[i].Scroll_Texture();
		}


	}
}
