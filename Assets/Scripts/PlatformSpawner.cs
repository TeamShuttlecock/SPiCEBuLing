using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour {

    public List<GameObject> BrickPrefabs = new List<GameObject>();

    public int FloorNumber = 0;
    public int BrickNumberPerFloorMin = 0;
    public int BrickNumberPerFloorMax = 0;

    public float IntervalX = 0.0f;
    public float IntervalY = 0.0f;

    protected List<List<Brick>> BrickCollection = new List<List<Brick>>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnPlatforms()
    {
        int brickNum = 0;

        for (int i = 0; i < FloorNumber; i++)
        {
            brickNum = Random.Range(BrickNumberPerFloorMin, BrickNumberPerFloorMax);

            for (int j = 0; j < brickNum; j++)
            {
                //Vector3 pos = new Vector3()
            }
        }
    }
}
