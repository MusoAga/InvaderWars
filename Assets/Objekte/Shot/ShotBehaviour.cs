using UnityEngine;
using System.Collections;

public class ShotBehaviour : MonoBehaviour {

    private int lifetime;
	
	// Update is called once per frame
	void Update () {
        lifetime++;
        if (lifetime > 100)
            DestroyObject(gameObject);
	}
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null)
            coll.gameObject.GetComponent<Hitable>().onHit();
        Destroy(gameObject);
    }
}
