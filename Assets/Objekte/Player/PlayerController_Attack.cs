using UnityEngine;
using System.Collections;


public class PlayerController_Attack : PlayerController_Base {

    //Boundary
    private float xMinBoundary;
    private float xMaxBoundary;
    private float yMinBoundary;
    private float yMaxBoundary;

    // Update is called once per frame
    public override void Update ()
    {
        base.Update();
        // Steuerung-> Benutzer-Input auslesen und einer Variable zuweisen
        float forward = Input.GetAxis("Vertical");
		float sideward = Input.GetAxis("Horizontal");

        if (forward != 0 || sideward != 0)
        {
            Vector3 direction = this.transform.right * sideward + this.transform.up * forward;
            direction *= speed;
            moveInDirection(direction);
        }

		transform.position = new Vector3
			(
				Mathf.Clamp (transform.position.x, xMinBoundary, xMaxBoundary), 
				Mathf.Clamp (transform.position.y, yMinBoundary, yMaxBoundary),
				0.0f
				);
	
	}

    public override void Start()
    {
        base.Start();
        // +- 0.4 wegen dem Collider vom Spieler
        xMinBoundary = InvaderWars.boundXmin(gameObject);
        xMaxBoundary = InvaderWars.boundXmax(gameObject);
        yMinBoundary = InvaderWars.boundYmin(gameObject);
        yMaxBoundary = InvaderWars.boundYmax(gameObject);
    }

}
