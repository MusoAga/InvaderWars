using UnityEngine;
using System.Collections;

public class PoisonBehaviour : ShotBehaviour {

    protected float timer; // Lebenszeit des Giftes

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer++;
        if (timer > 500)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController_Base>() != null)
        { 
            Destroy(gameObject);
            coll.gameObject.GetComponent<PlayerController_Base>().onHit();
            coll.gameObject.GetComponent<Hitable>().dealDamage(1);
        }
    }

}
