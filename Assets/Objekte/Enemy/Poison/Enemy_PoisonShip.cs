using UnityEngine;
using System.Collections;

public class Enemy_PoisonShip : EnemyController_Base
{
    
    public override void enemyBehaviour()
   {
       //moveInDirection((-transform.up)*0.8f);
       moveInDirection((new Vector2(Random.value >= 0.5f ? (0.4f + Random.value) : -(0.7f + Random.value), -1)) * 0.8f);
        charge++;
        if (charge >= 30)
        {
            charge = 0;
            GameObject newPoison = Instantiate(shot);
            newPoison.transform.position = this.transform.position - this.transform.up*0.8f;
        }
    }
}
