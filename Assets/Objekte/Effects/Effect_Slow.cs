using UnityEngine;
using System.Collections;

public class Effect_Slow : MonoBehaviour
{

    private PlayerController_Base target; // Vom Effekt betroffener Spieler
    float lifetime = 120;

    // Use this for initialization
    void Start()
    {
        target = GetComponent<PlayerController_Base>();
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        target.GetComponent<Rigidbody2D>().velocity /= 2;
        target.speed /= 4;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime--;
        if (lifetime == 0)
        {
            Destroy(this);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            target.speed *= 4;
        }
    }
}
