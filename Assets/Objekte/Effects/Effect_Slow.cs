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
        if (target.getFrostResistence() == 0)
        {
            target.speed /= 4;
        }
        else if(target.getFrostResistence() == 1)
        {
            target.speed /= 3;
        }
        else if (target.getFrostResistence() == 2)
        {
            target.speed /= 2;
        }
        else if (target.getFrostResistence() == 3)
        {
            target.speed /= 1.5f;
        }


    }

    // Update is called once per frame
    void Update()
    {
        lifetime--;
        if (lifetime == 0)
        {
            Destroy(this);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (target.getFrostResistence() == 0)
            {
                target.speed *= 4;
            }
            else if (target.getFrostResistence() == 1)
            {
                target.speed *= 3;
            }
            else if (target.getFrostResistence() == 2)
            {
                target.speed *= 2;
            }
            else if (target.getFrostResistence() == 3)
            {
                target.speed *= 1.5f;
            }
        }
    }
}
