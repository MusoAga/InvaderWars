using UnityEngine;
using System.Collections;

public class PlayerController_Defense : PlayerController_Base {


    public override void Update()
    {
        base.Update();
        if (Input.GetAxis("Horizontal") != 0)
            moveInDirection(new Vector2(Input.GetAxis("Horizontal"), 0));
    }


    public override void moveInDirection(Vector2 direction)
    {
        transform.RotateAround(new Vector3(0, -5, 0), new Vector3(0, 0, (direction.x*-1)/30), 1);
    }

}