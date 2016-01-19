using UnityEngine;
using System.Collections;

public class Enemy_Flamethrower : EnemyController_Base {

    private Vector3 direction;

	// Use this for initialization
	void Start () {
        direction = transform.up;
        resources = 30;
	}

    // Feuere Schüsse ab
    public override void shoot()
    {
        base.shoot();
        GetComponent<Animator>().Play("Flamethrower_Shoot");
    }

    public override void enemyBehaviour()
    {
        moveInDirection(direction * speed);

        PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
        if (player != null)
        {
            transform.rotation = new Quaternion();
            transform.Rotate(0, 0, InvaderWars.getAngleBetweenTwoPoints(transform.position, player.transform.position));
            charge++;
            if (charge > 120)
                shoot();
        }
    }
    
}
