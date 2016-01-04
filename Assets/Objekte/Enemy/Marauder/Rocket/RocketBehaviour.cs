using UnityEngine;
using System.Collections;

public class RocketBehaviour : MonoBehaviour
{

    bool stopChasing = false;
    float timer;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer > 100)
        {
            Explosion.explode(gameObject);
            Destroy(gameObject);
            return;
        }

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

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<Hitable>() != null)
            coll.gameObject.GetComponent<Hitable>().onHit();
        Explosion.explode(gameObject);
        Destroy(gameObject);
    }
}
