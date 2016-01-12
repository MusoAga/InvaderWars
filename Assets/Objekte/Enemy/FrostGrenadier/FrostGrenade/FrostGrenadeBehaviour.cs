using UnityEngine;
using System.Collections;

public class FrostGrenadeBehaviour : ShotBehaviour {

    public GameObject frost;

    public override void shotBehaviour()
    {

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
        Explosion.explode(gameObject);
        Instantiate(frost, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
