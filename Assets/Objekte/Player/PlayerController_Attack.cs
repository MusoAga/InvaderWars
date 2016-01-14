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

        // Berechne die Boundary Dynamisch zu jeder Auflösung
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        // +- 0.4 wegen dem Collider vom Spieler
        xMinBoundary = bottomCorner.x + 0.4F;
        xMaxBoundary = topCorner.x - 0.4F;
        yMinBoundary = bottomCorner.y + 0.4F;
        yMaxBoundary = topCorner.y - 0.4F;
    }

}
