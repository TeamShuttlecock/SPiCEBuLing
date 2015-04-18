using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour {

    public List<GameObject> BrickPrefabs = new List<GameObject>();

    public int FloorNumber = 0;
    public int BrickNumberPerFloorMax = 0;

    public float IntervalX = 0.0f;
    public float IntervalY = 0.0f;

    public float BrickBaseWidth = 0.0f;

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

        Init();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.X))
        {
            ClearPlatforms();
            SpawnPlatforms();
        }
	}

    public void Init()
    {
        for(int i=0; i<FloorNumber; i++)
        {
            mBrickCollection.Add(new List<Brick>());
            for(int j=0; j<BrickNumberPerFloorMax; j++)
            {
                mBrickCollection[i].Add(null);
            }
        }
    }

    public void ClearPlatforms()
    {
        for (int i = 0; i < mBrickCollection.Count; i++)
        {
            if (mFloorRoots.Count <= i)
            {
                GameObject floorGo = new GameObject("Floor_" + i);
                Transform floorRoot = floorGo.GetComponent<Transform>();
                if (floorRoot == null)
                    floorRoot = floorGo.AddComponent<Transform>();
                floorRoot.position = Vector3.zero;
                floorRoot.localScale = Vector3.one;

                mFloorRoots.Add(floorRoot);
            }

            for (int j = 0; j < mBrickCollection[i].Count; j++ )
            {
                if (mBrickCollection[i][j] != null)
                {
                    CacheManager.Instance.DestroyCachedObject(mBrickCollection[i][j].gameObject);

                    mBrickCollection[i][j] = null;
                }
            }
        }
    }

    public void SpawnPlatforms()
    {
        float currentPosX = 0.0f;

        for (int y = 0; y < FloorNumber; y++)
        {
            currentPosX = 0.0f;

            int factor = BrickNumberPerFloorMax;

            for (int x = 0; x < BrickNumberPerFloorMax; x++)
            {
                int brickIdx = Random.Range(0, BrickPrefabs.Count);
                Vector3 pos = new Vector3(currentPosX + IntervalX + BrickBaseWidth / 2, (FloorNumber - y - 1) * IntervalY);
                currentPosX = pos.x;

                bool shouldSpawn = false;

                int upLeftX = x - 1;
                int upY = y - 1;

                int upRightX = x + 1;

                if(upY >= 0 && upY < FloorNumber)
                {
                    if (mBrickCollection[upY][x] != null)
                    {
                        shouldSpawn = false;
                    }
                    else
                    {
                        if (upLeftX >= 0 && upLeftX < BrickNumberPerFloorMax)
                        {
                            if (mBrickCollection[upY][upLeftX] != null)
                            {
                                shouldSpawn = true;
                            }
                        }
                        else if (upRightX >= 0 && upRightX < BrickNumberPerFloorMax)
                        {
                            if (mBrickCollection[upY][upRightX] != null)
                            {
                                shouldSpawn = true;
                            }
                        }
                    }
                }

                if(!shouldSpawn)
                {
                    shouldSpawn = Random.Range(0, factor) == 0;
                }

                if(shouldSpawn)
                {
                    GameObject brickGo = (GameObject)CacheManager.Instance.InstantiateObject(BrickPrefabs[brickIdx]);
                    Brick brick = brickGo.GetComponent<Brick>();
                    brickGo.transform.position = pos + transform.position; 
                    brickGo.transform.parent = mFloorRoots[y];

                    mBrickCollection[y][x] = brick;
                }
                else
                {
                    factor--;
                }
            }
        }
    }
}
