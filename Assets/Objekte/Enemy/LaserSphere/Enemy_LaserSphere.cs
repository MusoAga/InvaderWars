using UnityEngine;
using System.Collections;

public class Enemy_LaserSphere : EnemyController_Base {

	// Use this for initialization
	void Start () {
        transform.eulerAngles = new Vector3(0, 0, 225 - 90 * Random.Range(0, 2));
	}

    public override void enemyBehaviour()
    {
        moveInDirection(new Vector3(0, -1));
    }

    public override void onHit()
    {

    }
}
