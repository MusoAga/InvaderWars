using UnityEngine;
using System.Collections;

public class Enemy_PoisonShip : EnemyController_Base
{
    
    public override void enemyBehaviour()
   {
        base.enemyBehaviour();
        charge++;
        if (charge >= 30)
        {
            charge = 0;
            GameObject newPoison =Instantiate(shot, gameObject.transform.position, Quaternion.identity) as GameObject;
            newPoison.transform.position = this.transform.position - this.transform.up*0.8f;
        }
    }
}
