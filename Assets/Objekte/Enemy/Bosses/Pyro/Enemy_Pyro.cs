using UnityEngine;
using System.Collections;

public class Enemy_Pyro : BossController {

    private bool isCharging;

    public override void enemyBehaviour()
    {
        if (isCharging) return;
        charge++;
        PlayerController_Base player = FindObjectOfType<PlayerController_Base>();
        if (player == null) return;

        float oldCharge = charge;

        if (charge % 80 == 0)
                shoot();

        transform.eulerAngles = new Vector3(0, 0, InvaderWars.getAngleBetweenTwoPoints(transform.position, player.transform.position)) ;
        if (Vector2.Distance(player.transform.position, transform.position) < 7)
            GetComponent<Rigidbody2D>().AddForce(-transform.up*4);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, InvaderWars.boundXmin(gameObject)+1, InvaderWars.boundXmax(gameObject)-1), Mathf.Clamp(transform.position.y, InvaderWars.boundYmin(gameObject)+1, InvaderWars.boundYmax(gameObject)-1), transform.position.z);

        charge = oldCharge;

        if (charge >= 500)
        {
            charge = 0;
            isCharging = true;
            GetComponent<Animator>().Play("Pyro_Charge");
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().velocity = new Vector2();
        }

    }
    

    public override void onHit()
    {

    }

    public void startCharge()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up*1500);
        StartCoroutine(stopCharging());
    }

    IEnumerator stopCharging()
    {
        yield return new WaitForSeconds(0.6f);
        isCharging = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2();
    }

}
