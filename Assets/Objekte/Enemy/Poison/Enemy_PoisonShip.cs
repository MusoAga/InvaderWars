using UnityEngine;
using System.Collections;

public class Enemy_PoisonShip : EnemyController_Defense
{
    protected float charge;
    public GameObject poison;

    // Use this for initialization
    void Start()
    {

    }

    public override void enemyBehaviour()
   {
       moveInDirection(transform.up*0.8f);
        charge++;
        if (charge >= 30)
        {
            charge = 0;
            GameObject newPoison = (GameObject)Instantiate(poison);
            newPoison.transform.position = this.transform.position - this.transform.up*0.8f;
        }
    }
}
