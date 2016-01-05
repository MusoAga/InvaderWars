using UnityEngine;
using System.Collections;

public class Enemy_Rogue : EnemyController_Base, Hitable
{

    private int loadedEnergy = 0;
    private Vector2 teleportPoint;
    private float playerPositionX;
    private float teleportPositionX;
    private PlayerController_Base player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController_Base>();
        UpdateTeleportPoint();
    }

    public override void enemyBehaviour()
    {
        loadedEnergy++;
        charge++;
        if (loadedEnergy > 15)
        {
            transform.position = teleportPoint;
            UpdateTeleportPoint();
            loadedEnergy = 0;
        }

        if (charge > 80)
            shoot();

    }

    private void UpdateTeleportPoint()
    {
        playerPositionX = player.transform.position.x;

        if ((playerPositionX - transform.position.x) < 2)
            teleportPositionX = playerPositionX * -1;
        else
            teleportPositionX = Random.Range(-4.5F, 4.5F);

        teleportPoint = new Vector2(teleportPositionX, Random.Range(0.7F, 1.4F));
    }
}
