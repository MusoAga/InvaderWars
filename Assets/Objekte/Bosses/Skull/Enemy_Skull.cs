using UnityEngine;
using System.Collections;

public class Enemy_Skull : EnemyController_Base
{

    protected Animator animator;
    private string currentState;
    private GameObject laser;

    // Use this for initialization
    public void Start()
    {
        animator = GetComponent<Animator>();
        lifepoints = 2;
    }

    public void idle()
    {
        currentState = "Idle";
    }

    public override void enemyBehaviour()
    {
        if (currentState == "Break") return;

        if (currentState == "Idle") charge++;
        
            if (currentState != "Mortar")
                transform.position = new Vector3(transform.position.x + Mathf.Cos(Time.frameCount / 200) / 100, transform.position.y+ Mathf.Sin(Time.frameCount/10)/100, transform.position.z);

        if (currentState == "Idle" && charge > 200)
        {
            charge = 0;
            if (Random.Range(0, 3) > 1)
            {
                currentState = "Laser";
                animator.Play("LaserCharge");
            }
            else
            {
                GetComponent<AudioSource>().Play();
                currentState = "Mortar";
                animator.Play("Mortar");
                laser = Instantiate(shot);
                laser.transform.position = transform.position;

                laser.transform.Rotate(0, 0, 180);
            }
        }

    }

    public void shootLaser()
    {
        laser = Instantiate(shot);
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 1, 0);
        laser = Instantiate(shot);
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 1, 0);
        laser.transform.Rotate(0, 0, 45);
        laser = Instantiate(shot);
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 1, 0);
        laser.transform.Rotate(0, 0, -45);
        GetComponent<AudioSource>().Play();
    }

    public void stopLaser()
    {
        currentState = "Idle";
        Destroy(laser.gameObject);
    }

    // Der Gegner wird getroffen
    public override void onHit()
    {
        if (currentState != "Laser")
            return;
        lifepoints--;
        if (lifepoints <= 0)
        {
            currentState = "Break";
            animator.Play("Break");
        }
    }

    public void mortar()
    {
        if (Time.frameCount % 80 != 0) return;
        GetComponent<AudioSource>().Play();
        laser = Instantiate(shot);
        PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
        laser.transform.localPosition = new Vector2(player.transform.position.x, player.transform.position.y+2);
    }

    public void onDestroy()
    {
        Explosion.explode(gameObject, 3);
        Destroy(gameObject);
    }
}
