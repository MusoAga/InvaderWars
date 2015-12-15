using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_Def : GameController {

    //List mit den noch aktiven Spielerbasen auf dem Todesstern.
    private List<GameObject> baseList = new List<GameObject>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initialiseLevel()
    {
        //baseList.Add(PlayerBase);
        //baseList.Add(PlayerBase);
        //baseList.Add(PlayerBase);

        createSpawnPoints();

    }

    public void removeBase(GameObject playerBase)
    {
        baseList.Remove(playerBase);
    }

}
