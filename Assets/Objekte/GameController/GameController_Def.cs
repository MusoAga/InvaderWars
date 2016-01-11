using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController_Def : GameController {

    //List mit den noch aktiven Spielerbasen auf dem Todesstern.
    private List<GameObject> baseList = new List<GameObject>();
    

    /** Timer, der die verbleibende Spielzeit für das Level repräsentiert */
    private int levelTimer = 10000;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        base.initialiseLevel(1);
    }
	
	// Update is called once per frame
	void Update () {
        base.checkMenus();
        
	}

    public void initialiseLevel()
    {
        //baseList.Add(PlayerBase);
        //baseList.Add(PlayerBase);
        //baseList.Add(PlayerBase);

        

    }

    public void removeBase(GameObject playerBase)
    {
        baseList.Remove(playerBase);
    }

}
