using UnityEngine;
using System.Collections;

public interface Hitable {
    void onHit();
    void dealDamage(int damage);
}
