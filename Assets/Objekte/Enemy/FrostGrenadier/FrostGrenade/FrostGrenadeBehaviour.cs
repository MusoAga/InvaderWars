using UnityEngine;
using System.Collections;

public class FrostGrenadeBehaviour : ShotBehaviour, Hitable {

    public GameObject frost;

    public override void shotBehaviour()
    {
        transform.Rotate(new Vector3(0, 0, 3));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController_Base>() != null)
        {
            coll.gameObject.GetComponent<PlayerController_Base>().dealDamage(1);
            onDestruction();
        }
    }

    public override void onDestruction()
    {
        Instantiate(frost, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void onHit()
    {
        Explosion.explode(gameObject);
        Destroy(gameObject);
    }

    public void dealDamage(int damage)
    {
        Explosion.explode(gameObject);
        Destroy(gameObject);
    }
}
