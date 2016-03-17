using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

    //IV
    public int maxEnemiesSpawned = 10;
    private int maxEnemies;
    private int currentEnemies = 0;
    private int totalEnemiesSpawned = 0;
    private float timer;
    public GameObject[] enemies = new GameObject[4];
    private bool isSpawning = false;
    private GameObject enemy;
    private GameObject GameController;
    private bool finished = false;

    public void setup(GameObject[] _enemies, int difficulty)
    {
        this.enemies = _enemies;
        
        switch (difficulty)
        {
            case 1:
                this.maxEnemies = 2;
                this.timer = 5f;
                maxEnemiesSpawned = 10;
                break;
            case 2:
                this.maxEnemies = 3;
                this.timer = 4.5f;
                maxEnemiesSpawned = 12;
                break;
            case 3:
                this.maxEnemies = 5;
                this.timer = 4f;
                maxEnemiesSpawned = 15;
                break;
            case 4:
                this.maxEnemies = 6;
                this.timer = 3.5f;
                maxEnemiesSpawned = 18;
                break;
            case 5:
                this.maxEnemies = 8;
                this.timer = 3f;
                maxEnemiesSpawned = 20;
                break;
            default:
                this.maxEnemies = 5;
                this.timer = 4f;
                maxEnemiesSpawned = 12;
                break;
        }
    }

    private IEnumerator spawnRandomEnemy() 
    {
        //print(maxEnemies.ToString());
        float rnd = Random.value;
        isSpawning = true;
        //Spawndelay abwarten
        yield return new WaitForSeconds(timer + (Random.value >= 0.5f ? (Random.value * 2) : -(Random.value) / 6));
        enemy = null;
        if(currentEnemies < maxEnemies)
        {
           totalEnemiesSpawned++;
           currentEnemies++;
           if(totalEnemiesSpawned <= 4)
           {
              enemy = enemies[0];
           }
           else if(totalEnemiesSpawned > 4 & totalEnemiesSpawned <= 6)
           {
               if(rnd >= 0.65f)
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
               if(rnd <= 0.5f)
               {
                   enemy = enemies[0];
               }
               else if(rnd > 0.5f & rnd <= 0.7f)
               {
                   enemy = enemies[1];
               }
               else if(rnd > 0.7f & rnd <= 1.0f)
               {
                   enemy = enemies[2];
               }
           }
               enemy = Instantiate(enemy, gameObject.transform.position, enemy.transform.rotation) as GameObject;
               enemy.GetComponent<EnemyController_Base>().setSpawnOrigin(gameObject);
        }
        isSpawning = false;
    }

    public void spawnBoss()
    {
        Instantiate(enemies[3], new Vector2(0.0f, 3.5f), enemies[3].transform.rotation);
        //StartCoroutine(instantiateBoss());
    }

    private IEnumerator instantiateBoss()
    {
        yield return new WaitForSeconds(2.0f);
        

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
        GameController = GameObject.Find("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	    if(!isSpawning && totalEnemiesSpawned < maxEnemiesSpawned)
        {
            //print("Something");
            StartCoroutine(spawnRandomEnemy());
           // ausgabe();
        }
        if (totalEnemiesSpawned >= maxEnemiesSpawned && !finished)
        {
            GameController.GetComponent<GameController>().spawnFinished();
            finished = true;
        }
	}
}
