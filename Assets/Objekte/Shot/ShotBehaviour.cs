using UnityEngine;
using System.Collections;

public class ShotBehaviour : MonoBehaviour {

    private int lifetime;
	
	// Update is called once per frame
	void Update () {
        lifetime++;
        if (lifetime > 50)
            DestroyObject(gameObject);
	}
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<EnemyController_Defense>() != null)
            coll.gameObject.GetComponent<EnemyController_Defense>().hit();
        Destroy(gameObject);
    }
}
