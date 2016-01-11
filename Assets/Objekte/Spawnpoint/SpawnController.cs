using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

    //IV
    private int maxEnemies;
    private int currentEnemies = 0;
    private int totalEnemiesSpawned = 0;
    private float timer;
    public GameObject[] enemies = new GameObject[4];
    private bool isSpawning = false;
    private GameObject enemy;

    public void setup(GameObject[] _enemies, int difficulty)
    {
        this.enemies = _enemies;
        switch (difficulty)
        {
            case 1:
                this.maxEnemies = 2;
                this.timer = 3f;
                break;
            case 2:
                this.maxEnemies = 3;
                this.timer = 2.5f;
                break;
            case 3:
                this.maxEnemies = 5;
                this.timer = 2.5f;
                break;
            case 4:
                this.maxEnemies = 6;
                this.timer = 2.5f;
                break;
            case 5:
                this.maxEnemies = 8;
                this.timer = 2.0f;
                break;
            default:
                this.maxEnemies = 5;
                this.timer = 2.5f;
                break;
        }
    }

    private IEnumerator spawnRandomEnemy() 
    {
        //print(maxEnemies.ToString());
        float rnd = Random.value;
        isSpawning = true;
        //Spawndelay abwarten
        yield return new WaitForSeconds(timer + (Random.value * 2));
        enemy = null;
        if(currentEnemies < maxEnemies)
        {
           totalEnemiesSpawned++;
           currentEnemies++;
           if(totalEnemiesSpawned <= 4)
           {
              enemy = enemies[0];
           }
           else if(totalEnemiesSpawned > 4 && totalEnemiesSpawned <= 6)
           {
               if(rnd >= 0.51f)
               {
                   enemy = enemies[0];
               }
               else
               {
                   enemy = enemies[1];
               }
          }
          else if(totalEnemiesSpawned > 6)
          {
               if(rnd <= 0.125f)
               {
                   enemy = enemies[0];
               }
               else if(rnd > 0.125f && rnd <= 0.4f)
               {
                   enemy = enemies[1];
               }
               else if(rnd > 0.4f && rnd <= 0.8f)
               {
                   enemy = enemies[2];
               }
               else if(rnd > 0.8f && rnd < 1.0f)
               {
                   enemy = enemies[3];
               }
           }
           print(enemy);
           enemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity) as GameObject;
           enemy.GetComponent<EnemyController_Base>().setSpawnOrigin(gameObject);
        }
        isSpawning = false;
    }

    public void enemyDied() 
    {
        if(currentEnemies > 0)
        currentEnemies--;
    }

    private void ausgabe() 
    {
        string ausgabeString = "Max: " +  maxEnemies.ToString() + "\nCurrent: " + currentEnemies.ToString() + "\nTotal: " + totalEnemiesSpawned.ToString() + "\n";
        print(ausgabeString);    
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if(!isSpawning)
        {
            //print("Something");
            StartCoroutine(spawnRandomEnemy());
           // ausgabe();
        }
	}
}
