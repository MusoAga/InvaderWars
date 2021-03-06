﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Rogue : BossController, Hitable
{

    private int teleportIntervall = 0;
    private int teleportEnergy = 450;
    private Vector2 teleportPoint;
    private float playerPositionX;
    private float teleportPositionX;
    private PlayerController_Base player;
    private bool attackMode = false;
    public Slider energySlider;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController_Base>();

        UpdateTeleportPoint();

        energySlider.maxValue = teleportEnergy;
        energySlider.minValue = 0;

        lifepoints = 1;
        bossHealthBar.maxValue = lifepoints;
        bossHealthBar.minValue = 0;
    }

    public override void enemyBehaviour()
    {
        if(player != null)
        {
            if (teleportEnergy > 0)
            {
                teleportEnergy--;
                energySlider.value = teleportEnergy;
                EnemyControl();
            }
            else
            {
                GetComponent<Animator>().Play("Cryo_Exhausted");
                StartCoroutine(LoadEnergy());
            }
        }
    }

    private void UpdateTeleportPoint()
    {
        playerPositionX = player.transform.position.x;

        if(!attackMode)
        {
            if (playerPositionX > -4.0 && playerPositionX < 0)
                teleportPositionX = Random.Range(0.5F, 4.0F);
            else
                teleportPositionX = Random.Range(-0.5F, -4.0F);

            teleportPoint = new Vector2(teleportPositionX, Random.Range(0.3F, 1.3F));
        } else
        {
            teleportPoint = new Vector2(playerPositionX - 0.4F, Random.Range(0.3F, 1.3F));
            attackMode = false;
        }
    }

    private void EnemyControl()
    {
        charge++;
        teleportIntervall++;

        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.frameCount / 10) / 100, transform.position.z);
        if (teleportIntervall > 55)
        {
            UpdateTeleportPoint();
            transform.position = teleportPoint;
            teleportIntervall = 0;
        }
        else if (charge > 150)
        {
            attackMode = true;
            UpdateTeleportPoint();
            transform.position = teleportPoint;
            shoot();
            GetComponent<Animator>().Play("Cryo_Shoot");
        }
    }


    // Feuere Schüsse ab
    public override void shoot()
    {
        // Sound abspielen
        charge = 0;
        GetComponent<AudioSource>().PlayOneShot(shootSound);
        GameObject fire = (GameObject)Instantiate(shot, transform.localPosition + transform.up / 2, transform.localRotation);
        fire.GetComponent<ShotBehaviour>().shoot(gameObject);
    }

    IEnumerator LoadEnergy()
    {
        yield return new WaitForSeconds(4);
        teleportEnergy = 450;
        energySlider.value = teleportEnergy;
        GetComponent<Animator>().Play("Cryo_Idle");
    }
    
}
