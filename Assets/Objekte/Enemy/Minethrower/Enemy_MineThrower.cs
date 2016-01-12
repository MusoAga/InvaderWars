using UnityEngine;
using System.Collections;

public class Enemy_MineThrower : EnemyController_Base {

    void Start()
    {
        lifepoints = 3;
    }

    void dropMine()
    {
        charge = 0;
        GameObject mine = Instantiate(shot);
        mine.transform.position = transform.position-transform.up*0.6f;
        mine.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30), 20));
    }

    public override void enemyBehaviour()
    {
        base.enemyBehaviour();
        charge++;
        if (charge >= 150)
            dropMine();
    }

}
