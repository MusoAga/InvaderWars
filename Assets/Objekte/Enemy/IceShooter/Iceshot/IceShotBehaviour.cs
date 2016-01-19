using UnityEngine;
using System.Collections;

public class IceShotBehaviour : ShotBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null && coll.gameObject != owner)
        {
            coll.gameObject.GetComponent<Hitable>().dealDamage(damage);
            if (coll.GetComponent<Effect_Slow>() == null)
                coll.gameObject.AddComponent<Effect_Slow>();
            Destroy(gameObject);
        }
    }

}
