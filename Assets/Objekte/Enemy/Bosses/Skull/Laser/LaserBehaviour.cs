using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer++;
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y + 0.1f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0, GetComponent<BoxCollider2D>().size.y / 2f);
        if (timer > 80)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null)
            coll.gameObject.GetComponent<Hitable>().dealDamage(3);
    }

}
