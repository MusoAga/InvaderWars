using UnityEngine;
using System.Collections;

public class PlayerBase_Def : MonoBehaviour {


    //IV

    public GameObject explosion; //Referenz auf eine Explosion
    public int protectionPoints; //Lebenspunkte des Schilds
    public int lifePoints;
    private GameController_Def gameManager;
    //IM

    private void hit(int damage)
    {
        if (protectionPoints <= 0)
        {
            lifePoints -= damage;
        }
        else
        {
            protectionPoints -= damage;
        } 
    }

    private void onBaseDestruction()
    {
        if (lifePoints <= 0)
        {
            gameManager.removeBase(gameObject);         //Nimmt diese Schwachstelle aus der Liste der aktiven Schwachstellen
            gameObject.SetActive(false);                //Deaktiviert diese Schwachstelle
        }
    }

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameController_Def>();
	}
	
	// Update is called once per frame
	void Update () {
        onBaseDestruction();
	}
}
