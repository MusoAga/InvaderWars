using UnityEngine;
using System.Collections;

public interface Hitable {
    void onHit();
    void OnCollisionEnter2D(Collision2D coll);
}
