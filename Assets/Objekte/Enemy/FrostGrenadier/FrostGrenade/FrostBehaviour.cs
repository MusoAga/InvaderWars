using UnityEngine;
using System.Collections;

public class FrostBehaviour : MonoBehaviour {

     float lifetime = 600;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        lifetime--;
        if (lifetime == 0)
            Destroy(gameObject);

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController_Base>() != null)
            if (coll.gameObject.GetComponent<Effect_Slow>() == null)
            coll.gameObject.AddComponent<Effect_Slow>();
    }
}
