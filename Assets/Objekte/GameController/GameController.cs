using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GameController : MonoBehaviour {

    /** Int mit Zahl des aktuellen Levels. Wird zur Initiierung des Levels beim Laden benutzt. */
    private int currentLevel;
    private int currentDifficulty;

    /** List mit allen SpawnPoints des aktuellen Levels. */
    //private List<SpawnPoints> spawnPoints;

    /** Referenz auf das aktuelle Spielerobjekt */

    //private GameObject player;
    public GameObject player;

    public GameObject pauseMenu, winMenu, loseMenu;

    public Text collectedRessourcesValueText = null;
    public Text totalRessourcesValueText = null;


    /** Die Gesamtzahl der vom Player gesammelten Ressourcen in diesem Level.  */
    private int collectedResources = 0;
    /** Die insgesamt Gesammelten Resourcen im ganzen Spiel */
    private int totalResources = 0;

    /** Boolean-Werte für Spieler Gewonnen bzw. Verloren*/
    private bool playerLose = false;
    private bool playerWin = false;
    private bool pause = false;


    public Text PlanetInfo;
    public Text PlanetName;

    //Referenz auf den InitController des Levels
    public GameObject InitController;
    public GameObject spawnpoint;
    private Dictionary<string, string> levelInfo;
    private GameObject[] enemies;
    private SortedList<int, GameObject> spawnpoints = new SortedList<int, GameObject>();
    private int spawningSpawnpoints = 99;
    private bool bossSpawned = false;

    // Use this for initialization
    void Start () {

        //player = GetComponent<PlayerController_Base>();


        //make sure menus are disabled
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);

        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
        OnLevelWasLoaded(Application.loadedLevel);
    }
	
	// Update is called once per frame
	void Update () {
        checkMenus();
        if (checkSpawnsFinished() && !bossSpawned)
        {
            print("boss gespawnt");
            foreach(KeyValuePair<int, GameObject> pair in spawnpoints){
                if (pair.Key.Equals(1))
                {
                    pair.Value.GetComponent<SpawnController>().spawnBoss();
                }
            }
            bossSpawned = true;
        }
        
    }

    /**
    Methode um überall auf dem Screen zu zeichnen.
    Wird in jedem Frame aufgerufen.
    */
    void OnGUI()
    {
        GUI.TextArea(new Rect((Screen.width - 200), 30, 150, 20), " Resources: " + totalResources.ToString());
    }

    /**
    Fügt der spawnPoints Liste einen weiteren SpawnPoint anhand der List hinzu.
    */
    public void addSpawnPoint(List<string> fileInput)
    {
        /*foreach (string entry in fileInput)
        {
            //spawnPoints.Add(entry);
        }*/
    }

    /**
    Erzeugt die SpawnPoints des Levels anhand der ArrayList spawnPoints.
    */
    public void createSpawnPoint(int x, int y, GameObject[] enemies)
    {

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
        collectedResources = 0;
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
            totalRessourcesValueText.text = totalResources.ToString();

            playerWin = false;
        }

        if (playerLose)
        {
            loseMenu.SetActive(true);
            playerLose = false;
        }

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
        Application.LoadLevel(Application.loadedLevel);
    }

    public void resumeLevel()
    {
        tooglePause();
    }


    public void quitLevel()
    {
        Application.LoadLevel("Planets");
    }

    public void setCurrentLevel(int i)
    {
        currentLevel = i;
    }

    public void startScene(string scene)
    {
        Application.LoadLevel(scene);
    }


    /*
    FÜR DIE PLANETENÜBERSICHT!
    */
    public void UpdatePlanetInfo(string planetName)
    {
        if (planetName.Equals("DroughtPlanet"))
        {
            PlanetInfo.text = "Auf diesem Planeten herrscht die Dürre.\nDie Sonne verbrennt dich, \nwenn du keinen Schutz hast!";
            PlanetName.text = "Zekila";
            currentLevel = 1;
        }
        else if (planetName.CompareTo("IcePlanet") == 0)
        {
            PlanetInfo.text = "Schnee, Eis und die Kälte.\nDie Umgebung macht das Leben\nfür die Raumschiffe schwer.";
            PlanetName.text = "Iconom";
            currentLevel = 2;
        }
        else if (planetName.CompareTo("StonePlanet") == 0)
        {
            PlanetInfo.text = "Stein, Stein und noch mal Stein.\nDich erwarten viele Felsen.\n";
            PlanetName.text = "Onix";
            currentLevel = 3;
        }
        else if (planetName.CompareTo("RockPlanet") == 0)
        {
            PlanetInfo.text = "Riesengroße Felsen bieten\nden Bewohnern guten Schutz.\nWie wirst du vorgehen?";
            PlanetName.text = "Rocky";
            currentLevel = 4;
        }
        else if (planetName.CompareTo("SteelPlanet") == 0)
        {
            PlanetInfo.text = "So hart war dein Gegner noch nie!\nÜberlege gut welche Waffen\ndu verwendest.";
            PlanetName.text = "Hardness";
            currentLevel = 5;
        }

    }

    public void spawnFinished()
    {
        spawningSpawnpoints--;
        print("Spawnpoint done");
        print(spawningSpawnpoints.ToString());
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
        string enemyPath = "Assets/Objekte/Enemy/";
        levelInfo = InitController.GetComponent<InitReaderController>().initialiseLevel(levelNumber);
       
        foreach (string info in levelInfo.Keys)
        {
            enemyPath = "Assets/Objekte/Enemy/";
            if (info.Contains("Enemytype_"))
            {
                enemyPath = "Assets/Objekte/Enemy/" + levelInfo[info] + ".prefab" ;
                enemies[enemyCounter] = AssetDatabase.LoadAssetAtPath(enemyPath, typeof(Object)) as GameObject;
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
        if(level == 4)
            initialiseLevel(currentLevel);
    }

    public void StartPlanetLevel()
    {
        Application.LoadLevel("rene_test");
       // StartCoroutine(initialiseLevel(currentLevel));
    }
    //-------------------------------------

}
