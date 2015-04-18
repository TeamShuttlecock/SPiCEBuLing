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

    protected List<List<Brick>> mBrickCollection = new List<List<Brick>>();
    protected List<Transform> mFloorRoots = new List<Transform>();

    private Transform _PlatformsRoot = null;
    private Transform PlatformsRoot
    {
        get
        {
            if (_PlatformsRoot == null)
            {
                GameObject go = new GameObject("PlatformsRoot");
                _PlatformsRoot = go.GetComponent<Transform>();
                if (_PlatformsRoot == null)
                    _PlatformsRoot = go.AddComponent<Transform>();
                _PlatformsRoot.position = Vector3.zero;
                _PlatformsRoot.localScale = Vector3.one;
            }
            return _PlatformsRoot;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.X))
        {
            SpawnPlatforms();
        }
	}

    public void SpawnPlatforms()
    {
        int brickNum = 0;

        float currentPosX = 0.0f;

        for (int y = 0; y < FloorNumber; y++)
        {
            currentPosX = 0.0f;
            brickNum = Random.Range(BrickNumberPerFloorMin, BrickNumberPerFloorMax);
            mBrickCollection.Add(new List<Brick>());

            if(mFloorRoots.Count <= y)
            {
                GameObject floorGo = new GameObject("Floor_" + y);
                Transform floorRoot = floorGo.GetComponent<Transform>();
                if (floorRoot == null)
                    floorRoot = floorGo.AddComponent<Transform>();
                floorRoot.position = Vector3.zero;
                floorRoot.localScale = Vector3.one;

                mFloorRoots.Add(floorRoot);
            }

            for (int x = 0; x < brickNum; x++)
            {
                int brickIdx = Random.Range(0, BrickPrefabs.Count);

                GameObject brickGo = (GameObject)CacheManager.Instance.InstantiateObject(BrickPrefabs[brickIdx]);
                Brick brick = brickGo.GetComponent<Brick>();

                Vector3 pos = new Vector3(currentPosX + IntervalX + brick.Width / 2, y * IntervalY);

                brickGo.transform.position = pos;
                brickGo.transform.parent = mFloorRoots[y];

                currentPosX = pos.x;
                mBrickCollection[y].Add(brick);
            }
        }
    }
}
