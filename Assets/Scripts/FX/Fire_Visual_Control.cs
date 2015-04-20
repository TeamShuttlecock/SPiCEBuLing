using UnityEngine;
using System.Collections;

public class Fire_Visual_Control : MonoBehaviour {

    public GameObject firePrefab, smokePrefab, extinctionPrefab;
    public int fireType =1;

    public void ChangeFireType(int curFireType)
    { 
        
        if (curFireType > 0 || curFireType < 4)
        {
            fireType = curFireType;
        }
        else return;
    }

    private void allFireType()
    {
        fireType = Mathf.Clamp(fireType, 1, 3);
        switch (fireType)
        { 
            case 1:
                firePrefab.SetActive(true);
                smokePrefab.SetActive(false);
                extinctionPrefab.SetActive(false);
                //print("nomal");
                break;
            case 2:
                firePrefab.SetActive(true);
                smokePrefab.SetActive(true);
                extinctionPrefab.SetActive(false);
                //print("smoking");
                break;
            case 3:
                firePrefab.SetActive(false);
                smokePrefab.SetActive(false);
                extinctionPrefab.SetActive(true);
                //print("extinction");
                break;
        }
    }
    private void Init()
    {
        firePrefab.SetActive(false);
        smokePrefab.SetActive(false);
        extinctionPrefab.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        allFireType();
	}
}
