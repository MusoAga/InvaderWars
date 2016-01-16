using UnityEngine;
using System.Collections;

public class Player_Laser_Behaviour : LaserBehaviour {

    private Collider2D playerColl;
    float timer;
    public int damage;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null & !coll.Equals(playerColl))
            coll.gameObject.GetComponent<Hitable>().dealDamage(damage);
    }

    // Use this for initialization
    void Start () {
        playerColl = GameObject.Find("Player").GetComponent<Collider2D>();
	}

    void FixedUpdate()
    {
        timer++;
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y + 0.3f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0, GetComponent<BoxCollider2D>().size.y / 1.5f);
        if (timer > 12)
            Destroy(gameObject);
    }
}
