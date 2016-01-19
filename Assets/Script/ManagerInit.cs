using UnityEngine;
using System.Collections;

public class ManagerInit : MonoBehaviour {

    public GameObject manager;
    private GameObject managerTest;
	// Use this for initialization
	void Awake () {

        managerTest = GameObject.Find("GameController");
        if(managerTest == null)
        {
           managerTest =  Instantiate(manager, Vector3.zero, Quaternion.identity) as GameObject;
            managerTest.name = "GameController";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
