using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    /** Int mit Zahl des aktuellen Levels. Wird zur Initiierung des Levels beim Laden benutzt. */
    private int currentLevel;

    /** List mit allen SpawnPoints des aktuellen Levels. */
    //private List<SpawnPoints> spawnPoints;

    /** Referenz auf das aktuelle Spielerobjekt */
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

    // Use this for initialization
    void Start () {

        //make sure menus are disabled
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        checkMenus();
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
    public void createSpawnPoints()
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

    public void startScene(string scene)
    {
        Application.LoadLevel(scene);
    }


    /*
    FÜR DIE PLANETENÜBERSICHT!
    */
    public void UpdatePlanetInfo(string planetName)
    {
        if (planetName.CompareTo("DroughtPlanet") == 0)
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

    public void StartPlanetLevel()
    {
        Application.LoadLevel("Defense");
    }
    //-------------------------------------

}
