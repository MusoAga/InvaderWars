using UnityEngine;
using System.Collections;

public class Enemy_Marauder : EnemyController_Base
{

    protected Vector2 targetPoint;

    void Start()
    {
        targetPoint = new Vector2(Random.Range(0, 2), Random.Range(0, 2));
        //Instantiate(shot).transform.position = targetPoint;
    }

    public override void enemyBehaviour()
    {
        moveTowardsPoint(targetPoint, 1);
        PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
        if (player != null)
        {
            transform.rotation = new Quaternion();
            transform.Rotate(0, 0, InvaderWars.getAngleBetweenTwoPoints(transform.position, player.transform.position));
            charge++;
            if (charge > 80)
                shoot();
        }
    }
}
