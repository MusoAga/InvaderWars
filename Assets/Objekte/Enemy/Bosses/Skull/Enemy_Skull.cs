using UnityEngine;
using System.Collections;

public class Enemy_Skull : BossController
{

    protected Animator animator;
    private string currentState;
    private GameObject laser;
    private AudioSource laserSound;

    // Use this for initialization
    public void Start()
    {
        animator = GetComponent<Animator>();
        lifepoints = 2;
        currentState = "Idle";
       laserSound = GetComponents<AudioSource>()[1];
    }

    public void idle()
    {
        if (currentState != "Break")
        currentState = "Idle";
    }

    public override void enemyBehaviour()
    {
        if (currentState == "Break") return;

        if (currentState == "Idle")
        {
            charge++;
            transform.eulerAngles = new Vector3();
            transform.position = new Vector3(transform.position.x + Mathf.Cos(Time.frameCount / 200) / 100, transform.position.y + Mathf.Sin(Time.frameCount / 10) / 100, transform.position.z);
            if (charge > 100)
                if (Random.Range(0, 2) == 1)
            {
                charge = 0;
                currentState = "Laser";
                animator.Play("Skull_ChargeLaser");
            }
                else
                {
                    charge = 0;
                    currentState = "Beam";
                    animator.Play("Skull_ChargeBeam");
                    GetComponent<AudioSource>().Play();
                    PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
                    if (player != null)
                        transform.eulerAngles = new Vector3(0, 0, InvaderWars.getAngleBetweenTwoPoints(player.transform.position, transform.position));
                }
        }

    }

    public void shootLaser()
    {
        laser = Instantiate(shot);
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 0.3f, 0);
        laser = Instantiate(shot);
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 0.3f, 0);
        laser.transform.Rotate(0, 0, 45);
        laser = Instantiate(shot);
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 0.3f, 0);
        laser.transform.Rotate(0, 0, -45);
        laserSound.Play();
    }

    public void shootBeam()
    {
        PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
        if (player != null)
            transform.eulerAngles = new Vector3(0, 0, InvaderWars.getAngleBetweenTwoPoints(player.transform.position, transform.position));
        if (Time.frameCount % 10 != 0) return;
        laser = Instantiate(shot);
        laserSound.Play();
        laser.transform.localPosition = transform.position - laser.transform.position + new Vector3(0.2f, 1, 0);
        laser.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z+180);
    }

    // Der Gegner wird getroffen
    public override void dealDamage(int damage)
    {
        if (currentState != "Laser")
            return;
        lifepoints--;
        if (lifepoints <= 0)
        {
            currentState = "Break";
            animator.Play("Skull_Break");
        }
    }


    public override void onDestruction()
    {
        Explosion.explode(gameObject, 3);
        Destroy(gameObject);
        base.onDestruction();
    }
}
