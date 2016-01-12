using UnityEngine;
using System.Collections;

public class FrostboltBehaviour : ShotBehaviour {

    public GameObject frost;

    void Start()
    {
        transform.Rotate(0, 0, 180);
    }

    public override void onDestruction()
    {
        Instantiate(frost, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
