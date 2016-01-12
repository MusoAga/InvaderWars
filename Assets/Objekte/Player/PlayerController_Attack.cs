using UnityEngine;
using System.Collections;


public class PlayerController_Attack : PlayerController_Base {
    
	//Boundary
	private float xMinBoundary = -4;
    private float xMaxBoundary = 4;
    private float yMinBoundary = -4;
    private float yMaxBoundary = 4;

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
            if (gameObject.GetComponent<Effect_Stun>() != null)
                direction /= 8;
            moveInDirection(direction);
        }

		transform.position = new Vector3
			(
				Mathf.Clamp (transform.position.x, xMinBoundary, xMaxBoundary), 
				Mathf.Clamp (transform.position.y, yMinBoundary, yMaxBoundary),
				0.0f
				);
	
	}
}
