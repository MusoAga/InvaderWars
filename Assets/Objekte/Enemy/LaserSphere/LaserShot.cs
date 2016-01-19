using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<PlayerController_Base>() != null)
            coll.gameObject.GetComponent<Hitable>().dealDamage(1);
    }
}
