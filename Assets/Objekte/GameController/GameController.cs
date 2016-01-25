using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameController : MonoBehaviour {

    /** Int mit Zahl des aktuellen Levels. Wird zur Initiierung des Levels beim Laden benutzt. */
    private int currentLevel;
    private int currentDifficulty;
    private int levelTemp = 1;
    private bool levelComplete = false;

    public GameObject pauseMenu, winMenu, loseMenu, planetTemp;

    public Text collectedRessourcesValueText = null;
    public Text totalRessourcesValueText = null;

    // Ressourcen die Ingame und in der planetenübersicht angezeigt werden
    private GameObject collectedValue, totalValue;

    /** Die Gesamtzahl der vom Player gesammelten Ressourcen in diesem Level.  */
    private int collectedResources = 0;
    /** Die insgesamt Gesammelten Resourcen im ganzen Spiel */
    private int totalResources = 0;

    /** Boolean-Werte für Spieler Gewonnen bzw. Verloren*/
    private bool playerLose = false;
    private bool playerWin = false;
    private bool pause = false;

    //Referenzen auf die Spawning- bzw. Initialisierungskomponenten
    public GameObject InitController;
    public GameObject spawnpoint;
    //Variable für die Übernahme der vom InitReader ausgelesenen Levelinfos
    private Dictionary<string, string> levelInfo;
    //Variable mit den Gegnertypen
    private GameObject[] enemies;
    //Variable zum speichern der Spawnpoints
    private SortedList<int, GameObject> spawnpoints = new SortedList<int, GameObject>();
    //Anzahl der noch aktiven Spawnpoints; 99 ist der Initialwert
    private int spawningSpawnpoints = 99;
    //Bool zur Feststellung, ob der Boss gespawnt wurde
    private bool bossSpawned = false;
    private bool gameCreated = false;

    //Variablen für Spielerupgrades
    public GameObject player;
    private GameObject upgradeSlider;
    private int lifeUpgrade = 3;
    private int firerateUpgrade = 3;
    private int speedUpgrade = 3;
    private int frostResistance = 3;
    private bool laserUpgrade = false;
    public AudioClip unableSound;

    //Variablen für die 
    private List<GameObject> BaseList = new List<GameObject>();

    // Static singleton property
    public static GameController Instance { get; private set; }

    // Use this for initialization
    void Start () {

        //make sure menus are disabled
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);

        currentLevel = 2;

        Time.timeScale = 1;
        //DontDestroyOnLoad(gameObject);
        OnLevelWasLoaded(Application.loadedLevel);
        player.GetComponent<PlayerController_Attack>().resetPlayerStats();
    }
	
	// Update is called once per frame
	void Update () {
        checkMenus();
        checkLevels();
        showRessources();
        if (!bossSpawned && checkSpawnsFinished())
        {
            spawningSpawnpoints = 99;
            bossSpawned = true;
            print("boss gespawnt");
            foreach(KeyValuePair<int, GameObject> pair in spawnpoints){
                if (pair.Key.Equals(1))
                {
                    bossSpawned = true;
                    pair.Value.GetComponent<SpawnController>().spawnBoss();
                    bossSpawned = true;
                }
            }
            bossSpawned = true;
        }
    }

    /*
    Wird vor der Start Funktion aufgerufen. Wird aufgerufen nachdem alle Objekte initialisiert sind.
    In diesem Fall wird sicher gegangen, dass es nicht 2 GameController in einem Level existieren
    */
    void Awake()
    {
        // Überprüfen ob es einen Konflikt gibt zwischen einen anderen GameController
        if (Instance != null && Instance != this)
        {
            // Falls ja, zerstöre ihn
            Destroy(gameObject);
        }

        // Setze die Singleton
        Instance = this;

        // Das Objekt nicht zerstören wenn man zwischen den Scenen navigiert
        DontDestroyOnLoad(gameObject);
    }

    /**
    Wird durch das Spielerobjekt aufgerufen, sobald dieses zerstört wurde und ruft onEnemyWin() auf, 
    um den Lose-Screen zu aktivieren. 
    Ruft zudem mit einer gewissen Chance initiateDefense() (Nur Angriff) auf, 
    um ein Verteidigungsszenario zu aktivieren. Ruft updateGameStats() auf, 
    um das File mit den Spielerstatistiken zu updaten.
    */
    public void onPlayerDestruction()
    {
        onEnemyWin();
        updateGameStats();
    }

    /**
    Wird bei Ablauf des Timers aufgerufen, sobald alle Feinde zerstört wurden. 
    Aktiviert den Win-Screen und ruft updateGameStats() auf, um das File mit den Spielerstatistiken zu updaten.
    */
    public void onVictory()
    {
        // Methode/Variable für alle gegner?
        updateGameStats();
        Time.timeScale = 0;
        playerLose = false;
        playerWin = true;
    }

    /**
    Kontrolliert den Timer-Status und sendet eine onVictory() 
    Nachricht an den GameManager, sobald der Timer abgelaufen und kein Gegner mehr auf dem Feld ist.
    */
    public void onTimeOver()
    {
        onVictory();
    }

    /**
    Wird von onPlayerDestruction() des GameManagers und onBaseReached() 
    der Gegner (Nur Verteidigung) aufgerufen und aktiviert den Lose-Screen.
    */
    public void onEnemyWin()
    {
        Time.timeScale = 0;
        playerWin = false;
        playerLose = true;
    }

    /**
    Wird beim Beenden eines Levels aufgerufen und 
    aktualisiert die Spielerstatistiken, die in einem externeren File gespeichert sind.
    */
    public void updateGameStats()
    {
        totalResources += collectedResources;
    }

    /**
    Wird vom Gegner aufgerufen, sobald dieser stirbt. 
    Addiert die übergebene Menge an Ressourcen auf die Anzahl von collectedResources.
    */
    public void addResources(int resources)
    {
        collectedResources += resources;
    }

    public void checkMenus()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            tooglePause();

        if (pause)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);

        if (playerWin)
        {

            winMenu.SetActive(true);

            collectedRessourcesValueText.text = collectedResources.ToString();

            totalResources += collectedResources;
            totalRessourcesValueText.text = totalResources.ToString();

            
            collectedResources = 0;
            playerWin = false;
        }

        if (playerLose)
        {
            loseMenu.SetActive(true);

            //totalResources += collectedResources;
            collectedResources = 0;
            playerLose = false;
        }

    }

    private void showRessources()
    {

        if (collectedValue != null)
            collectedValue.GetComponent<Text>().text = collectedResources.ToString();

        if (totalValue != null)
            totalValue.GetComponent<Text>().text = totalResources.ToString();
    }

    public bool isPaused()
    {
        return pause;
    }

    public bool won()
    {
        return playerWin;
    }

    public bool lose()
    {
        return playerLose;
    }

    public bool bossIsSpawned()
    {
        return bossSpawned;
    }

    public void setBossSpwaned(bool spawned)
    {
        bossSpawned = false;
    }

    public void tooglePause()
    {
        if (pause)
        {
            pause = false;
            Time.timeScale = 1;
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
        }
    }

    public void restartLevel()
    {
        spawningSpawnpoints = 99;
        spawnpoints.Clear();
        Application.LoadLevel(Application.loadedLevel);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        pause = false;
        Time.timeScale = 1;
    }

    public void resumeLevel()
    {
        tooglePause();
    }


    public void quitLevel()
    {
        Application.LoadLevel("Planets");
        spawningSpawnpoints = 99;
        spawnpoints.Clear();
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        pause = false;
    }

    public void setCurrentLevel(int i)
    {
        currentLevel = i;
    }

    public void checkLevels()
    {
        if(levelTemp == 2)
        {
            planetTemp = GameObject.Find("IcePlanet");
            planetTemp.GetComponent<Button>().interactable = true;
        }
        else if (levelTemp == 3)
        {
            planetTemp = GameObject.Find("IcePlanet");
            planetTemp.GetComponent<Button>().interactable = true;
            planetTemp = GameObject.Find("VolcanoPlanet");
            planetTemp.GetComponent<Button>().interactable = true;
        }
    }

    public void unlockNextLevel()
    {
        if(levelComplete)
        {

            levelTemp += 1;

            if (levelTemp == 2)
            {
                planetTemp = GameObject.Find("IcePlanet");
                planetTemp.GetComponent<Button>().interactable = true;
            }
            else if (levelTemp == 3)
            {
                planetTemp = GameObject.Find("VolcanoPlanet");
                planetTemp.GetComponent<Button>().interactable = true;
            }

            planetTemp = null;
            print("Nächstes Level freigeschaltet");
        }
    }

    public void setLevelComplete(bool complete)
    {
        levelComplete = complete;
    }

    public bool isLevelComplete()
    {
        return levelComplete;
    }

    public void startScene(string scene)
    {
        Application.LoadLevel(scene);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        pause = false;
        unlockNextLevel();
    }

    public void spawnFinished()
    {
        spawningSpawnpoints--;
        print("Spawnpoint done");
        print("Spawnpoints left: " + spawningSpawnpoints.ToString());
    }
    
    private bool checkSpawnsFinished()
    {
        if (spawningSpawnpoints == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void initialiseLevel(int levelNumber) 
    {
        enemies = new GameObject[4];
        currentDifficulty = 0;
        int enemyCounter = 0;
        int spawnpointCounter = 0;
        string[] spawnpointChoords = new string[2];
        string[] enemyPath = new string[5];
        enemyPath[1] = "Assets/Objekte/Enemy/Marauder";
        enemyPath[2] = "Assets/Objekte/Enemy/Minethrower";
        enemyPath[3] = "Assets/Objekte/Enemy/Poison";
        enemyPath[4] = "Assets/Objekte/Enemy/Bosses";
        levelInfo = InitController.GetComponent<InitReaderController>().initialiseLevel(levelNumber);

        foreach (string info in levelInfo.Keys)
        {
            //enemyPath[0] = "Assets/Objekte/Enemy";
            if (info.Contains("Enemytype_"))
            {

                // string[] guid = AssetDatabase.FindAssets(levelInfo[info],enemyPath);
                // if (!string.IsNullOrEmpty(guid[0]))
                // {
                //     print("nicht null");
                //     print(AssetDatabase.GUIDToAssetPath(guid[0]));
                //     enemies[enemyCounter] = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid[0]), typeof(Object)) as GameObject;
                //     enemyCounter++;
                // }
                // else

                //enemypath[0] = "assets/objekte/enemy/" + levelinfo[info] + ".prefab" ;
                enemies[enemyCounter] = Resources.Load(levelInfo[info]) as GameObject;
                // enemies[enemycounter] = assetdatabase.loadassetatpath(enemypath[0], typeof(object)) as gameobject;
                enemyCounter++;

            }
            else if (info.Equals("Difficulty"))
            {
                currentDifficulty = int.Parse(levelInfo[info]);
            }

            else if (info.Contains("Spawnpoint_"))
            {
               spawnpointChoords = levelInfo[info].Split(new char[] {','});
               GameObject Spawnpoint = Instantiate(spawnpoint, new Vector2(int.Parse(spawnpointChoords[0]), int.Parse(spawnpointChoords[1])), Quaternion.identity) as GameObject;
               Spawnpoint.GetComponent<SpawnController>().setup(enemies, currentDifficulty);
               spawnpointCounter++;
               spawnpoints.Add(spawnpointCounter, Spawnpoint);
                if (!gameCreated)
                {
                    gameCreated = true;
                }
            }
            else
            {
                continue;
            }
        }
        spawningSpawnpoints = spawnpoints.Count;
    }

    void OnLevelWasLoaded(int level)
    {
        collectedValue = GameObject.Find("collectedValue");
        totalValue = GameObject.Find("totalValue");

        if (level == 4)
            initialiseLevel(currentLevel);
        collectedResources = 0;
        print("Spawnpoints: " + spawningSpawnpoints.ToString());
    }

    public void StartPlanetLevel()
    {
        spawningSpawnpoints = 99;
        spawnpoints.Clear();
        Application.LoadLevel("rene_test");
        Time.timeScale = 1;
    }

    public void upgradePlayer(string upgrade)
    {
            //Lebensupgrade
            if (upgrade.Equals("Lifepoints") && lifeUpgrade > 0 && totalResources >= 300)
            {
                player.GetComponent<PlayerController_Attack>().increaseLifePoints();
                lifeUpgrade--;
                upgradeSlider = GameObject.Find("ArmorSlider");
                upgradeSlider.GetComponent<Slider>().value += 1;
                totalResources -= 300;
            }
            else if (upgrade.Equals("Lifepoints") && lifeUpgrade <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(unableSound);
            }

            //Upgrade der Feuerrate
            if (upgrade.Equals("Firerate") && firerateUpgrade > 0 && totalResources >= 200)
            {
                player.GetComponent<PlayerController_Attack>().increaseFireRate();
                firerateUpgrade--;
                upgradeSlider = GameObject.Find("FirerateSlider");
                upgradeSlider.GetComponent<Slider>().value += 1;
                totalResources -= 200;
            }
            else if (upgrade.Equals("Firerate") && firerateUpgrade <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(unableSound);
            }

            //Speedupgrade
            if (upgrade.Equals("Speed") && speedUpgrade > 0 && totalResources >= 250)
            {
                player.GetComponent<PlayerController_Attack>().increaseSpeed();
                speedUpgrade--;
                upgradeSlider = GameObject.Find("SpeedSlider");
                upgradeSlider.GetComponent<Slider>().value += 1;
                totalResources -= 250;
            }
            else if (upgrade.Equals("Speed") && speedUpgrade <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(unableSound);
            }


            //Upgrade der Frostresistenz
            if (upgrade.Equals("Frostresistenz") && frostResistance > 0 && totalResources >= 300)
            {
                player.GetComponent<PlayerController_Attack>().increaseFrostResistence();
                frostResistance--;
                upgradeSlider = GameObject.Find("FrostSlider");
                upgradeSlider.GetComponent<Slider>().value += 1;
                totalResources -= 300;
            }
            else if (upgrade.Equals("Frostresistenz") && frostResistance <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(unableSound);
            }

            //Upgrade der Frostresistenz
            if (upgrade.Equals("Schusswechsel") && laserUpgrade == false && totalResources >= 3000)
            {
                player.GetComponent<PlayerController_Attack>().changeShot("Laser");
                laserUpgrade = true;
                upgradeSlider = GameObject.Find("LaserSlider");
                upgradeSlider.GetComponent<Slider>().value += 1;
                totalResources -= 3000;
            }
            else if (upgrade.Equals("Schusswechsel") && laserUpgrade == true)
            {
                GetComponent<AudioSource>().PlayOneShot(unableSound);
            }

            upgradeSlider = null;
    }

   public void removePlayerBase(GameObject playerBase)
    {
        BaseList.Remove(playerBase);
    }
}
