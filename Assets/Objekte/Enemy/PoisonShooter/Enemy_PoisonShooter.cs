using UnityEngine;
using System.Collections;

public class Enemy_PoisonShooter : EnemyController_Base {

    void Start()
    {
        charge = 180;
    }

    public override void enemyBehaviour()
    {
        base.enemyBehaviour();
        charge++;
        if (charge >= 200)
            shoot();
    }
}
