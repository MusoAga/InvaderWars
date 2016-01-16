using UnityEngine;
using System.Collections;

public class ShotBehaviour : MonoBehaviour {

    private int lifetime;
    public int maxLifetime = 100;
    public float movementSpeed = 1;
    public int damage = 1;
    private GameObject owner;
	

    public void shoot(GameObject newOwner)
    {
        owner = newOwner;
    }

    public GameObject getOwner()
    {
        return this.owner;
    }

	// Update is called once per frame
	protected void Update () {
        lifetime++;
        shotBehaviour();
        if (lifetime > maxLifetime)
            onDestruction();
	}

    public virtual void shotBehaviour()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * movementSpeed;
    }

    public virtual void onDestruction() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null && coll.gameObject != owner)
        {
            coll.gameObject.GetComponent<Hitable>().dealDamage(damage);
            Destroy(gameObject);
        }
    }
}
