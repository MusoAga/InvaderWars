using UnityEngine;
using System.Collections;
using System;

public class EnemyController_Defense  : EnemyController_Base, Hitable {



	// Use this for initialization
	void Start () {
        // transform.Rotate(0, 0, Vector2.Angle(new Vector2(0, 0), new Vector2(transform.position.y, transform.position.x)), 0);
        transform.Rotate(new Vector3(0, 0, 180));
        //transform.Rotate(0, 0, Mathf.Atan2(-transform.position.y, -transform.position.x) * 180 / Mathf.PI - 90);
    }

}
