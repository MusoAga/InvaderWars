using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]


public class PlayerController_Attack : PlayerController_Base {

	CharacterController characterController;
	//Boundary
	public float xMinBoundary;
	public float xMaxBoundary;
	public float yMinBoundary;
	public float yMaxBoundary;

	//public float smooth = 1.0F;
	//public float jump;
	//private float startTime;

	// Use this for initialization
	public override void Start () {

		characterController = this.GetComponent<CharacterController>();
		//startTime = Time.time;
		
		//CharacterController dem Script zuweisen
		if(!characterController){
			
			Debug.LogError("PlayerController_Base.Start() " + name + "hat keinen CharacterController!");
			enabled = false;
		}

		//BasisKlasse angeben
		base.Start();
	
	}
	
	// Update is called once per frame
	public override void Update () {

		// Steuerung-> Benutzer Input auslesen und einer Variabel zuweisen
		float forward = Input.GetAxis("Vertical");
		float sideward = Input.GetAxis("Horizontal");

		//if (playerInput) {
		Vector3 direction = this.transform.right * sideward + this.transform.up * forward;
		characterController.Move (direction * speed);
		//}
		
		transform.position = new Vector3
			(
				Mathf.Clamp (transform.position.x, xMinBoundary, xMaxBoundary), 
				Mathf.Clamp (transform.position.y, yMinBoundary, yMaxBoundary),
				0.0f
				);
       


		/*transform.position = new Vector3transform.position.x
			(
				Mathf.Clamp (transform.position.x, Mathf.SmoothStep(xMinBoundary, xMinBoundary + jump, Time.deltaTime* smooth), xMaxBoundary), 
				Mathf.Clamp (transform.position.y, yMinBoundary, yMaxBoundary),
				0.0f
				
				);*/

		base.Update();
	
	}
}
