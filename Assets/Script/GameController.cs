using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    /** Int mit Zahl des aktuellen Levels. Wird zur Initiierung des Levels beim Laden benutzt. */
    private int currentLevel;

    /** List mit allen SpawnPoints des aktuellen Levels. */
    //private List<SpawnPoints> spawnPoints;

    /** Referenz auf das aktuelle Spielerobjekt */
    private GameObject player;

    /** Timer, der die verbleibende Spielzeit für das Level repräsentiert */
    // Woher kommt der Wert? 
    private int levelTimer = 10000;

    /** Die Gesamtzahl der vom Player gesammelten Ressourcen in diesem Level.  */
    private int collectedResources = 6000;
    /** Die insgesamt Gesammelten Resourcen im ganzen Spiel */
    private int totalResources;

    /** Boolean-Werte für Spieler Gewonnen bzw. Verloren*/
    private bool playerLose = false;
    private bool playerWin = false;
    private bool pause = false;

    public Texture loseWindow, winWindow;
    private float TextureWidth = 200;
    private float TextureHeight = 200;

    // Use this for initialization
    void Start () {
        //player = GetComponent<PlayerController_Base>();
        tooglePause();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
            tooglePause();

        if(!pause)
            onTimeOver();
    }

    /**
    Methode um überall auf dem Screen zu zeichnen.
    Wird in jedem Frame aufgerufen.
    */
    void OnGUI()
    {

        GUI.TextArea(new Rect((Screen.width - 200), 30, 150, 20), " Resources: " + collectedResources.ToString());

        if (playerLose)   
            GUI.DrawTexture(new Rect((Screen.width / 2) - (TextureWidth / 2), (Screen.height / 2) - (TextureHeight / 2), TextureWidth, TextureHeight), loseWindow);
        else if(playerWin)
            GUI.DrawTexture(new Rect((Screen.width / 2) - (TextureWidth / 2), (Screen.height / 2) - (TextureHeight / 2), TextureWidth, TextureHeight), winWindow);

        if(pause)
            GUI.DrawTexture(new Rect((Screen.width / 2) - (TextureWidth / 2), (Screen.height / 2) - (TextureHeight / 2), TextureWidth, TextureHeight), winWindow);

    }

    /**
    Fügt der spawnPoints Liste einen weiteren SpawnPoint anhand der List hinzu.
    */
    public void addSpawnPoint(List<string> fileInput)
    {
        foreach (string entry in fileInput)
        {
            //spawnPoints.Add(entry);
        }
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
    public void onPlayerDestructions()
    {
        if (true/*player.getLifepoints() <= 0*/)
        {
            onEnemyWin();
            updateGameStats();
        }
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
        levelTimer--;
        if(levelTimer == 0)
        {
            onVictory();
        }
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

}
