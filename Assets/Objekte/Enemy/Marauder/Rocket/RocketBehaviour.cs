using UnityEngine;
using System.Collections;
using System;

public class RocketBehaviour : ShotBehaviour, Hitable
{

    bool stopChasing = false;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2();
    }

    // Update is called once per frame
   public override void shotBehaviour() {
        if (!stopChasing)
        {
            PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
            if (player != null)
            {
                if (Vector2.Distance(player.transform.position, transform.position) > 1.5)
                    GetComponent<Rigidbody2D>().velocity = Vector3.Lerp(transform.position, player.transform.position, 1f) - transform.position;
                else
                    stopChasing = true;
                transform.rotation = new Quaternion();
                transform.Rotate(0, 0, Mathf.Atan2(transform.position.y - player.transform.position.y, transform.position.x - player.transform.position.x) * 180 / Mathf.PI + 90);
            }
        }
        else
            GetComponent<Rigidbody2D>().velocity = transform.up*4;
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null)
            coll.gameObject.GetComponent<Hitable>().dealDamage(damage);
        onDestruction();
    }

    public override void onDestruction()
    {
      Explosion.explode(gameObject);
      Destroy(gameObject);
    }

    public void onHit()
    {
        onDestruction();
    }

    public void dealDamage(int damage)
    {
        onDestruction();
    }
}
