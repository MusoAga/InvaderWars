using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using UnityEditor;

public class InitReaderController : MonoBehaviour
{
    //Nummer des aktuell betrachteten Level
    private int levelNumber;

    //Referenz auf die benötigten Xml-Dokumente
    public TextAsset enemyDoc;
    public TextAsset levelDoc;
    public TextAsset playerDoc;

    //Array mit den Dictionaries der einzelnen Feinde
    Dictionary<string, string>[] enemyDictionaries;

    //slicedSprite beinhaltet den bearbeiteten eigenen Sprite eines Gegners
    //hasOwnSprite ist true, wenn der betrachtete Gegner einen eigenen Sprite angegeben hat
    Sprite slicedSprite;
    private bool hasOwnSprite = false;

    //%-Zahl des Initialisierungsfortschitts
    int progress = 0;

    // Variabeln zur Erstellung der Gegnerprefabs
    // Ein Spritesheet wird als Object und nicht als Sprite angesehen
    private Object enemySprite;

    /*
     * Methode zur Ausgabe eines übergebenen Dictionarys
     * */
    private void printDictionaryContent(Dictionary<string, string> dic)
    {
        // print("Enemy: " + enemyDic["Name"]);
        foreach (string stat in dic.Keys)
        {
            print("\nInhalt: " + stat + " Wert: " + dic[stat]);
        }
    }


    /*
     * Läd die Werte aller Feinde aus dem Enemy.Xml und speichert sie pro Feind in einem Dictionary
     * 
     * */
    private void loadEnemies()
    {
        XmlDocument enemyXml = new XmlDocument();
        //Laden des XmlDocs
        enemyXml.LoadXml(enemyDoc.text);
        //Erstellt ein Array aus den einzelnen Levelnodes des XML
        XmlNodeList enemyNodes = enemyXml.GetElementsByTagName("Enemy"); 
        //Array nach Anzahl der Enemies initialisieren
        enemyDictionaries = new Dictionary<string, string>[enemyNodes.Count];
        //Zählvariable zum Zuweisen der einzelnen Gegner
        int i = 0;
        //Für jeden gefundenen Feind
        foreach (XmlNode enemyNode in enemyNodes)
        {
            //Array mit dem Inhalt der Enemy-Node füllen
            XmlNodeList nodeContent = enemyNode.ChildNodes;
            //Dictionary für den Feind erstellen
            enemyDictionaries[i] = new Dictionary<string, string>();

            foreach (XmlNode nodeItem in nodeContent)
            {
                {

                    if (nodeItem.Name == "Name")
                        enemyDictionaries[i].Add("Name", nodeItem.InnerText);

                    if (nodeItem.Name == "Type")
                        enemyDictionaries[i].Add("Type", nodeItem.InnerText);

                    if (nodeItem.Name == "Shoots")
                        enemyDictionaries[i].Add("Shoots", nodeItem.InnerText);

                    if (nodeItem.Name == "Speed")
                        enemyDictionaries[i].Add("Speed", nodeItem.InnerText);

                    if (nodeItem.Name == "Lifepoints")
                        enemyDictionaries[i].Add("LifePoints", nodeItem.InnerText);

                    if (nodeItem.Name == "Resources")
                        enemyDictionaries[i].Add("Resources", nodeItem.InnerText);

                    if (nodeItem.Name == "Sprite")
                        enemyDictionaries[i].Add("Sprite", nodeItem.InnerText);

                    if (nodeItem.Name == "Collidersize")
                        enemyDictionaries[i].Add("Collidersize", nodeItem.InnerText);
                }

            }
            if (i < enemyDictionaries.Length)
                i++;
        }

    }//End loadEnemies()


    /*
     * Erzeugt bzw. updatet die Gegnerprefabs
     * 
     * 
     * */

    private void createEnemyPreFabs()
    {
        string[] enemyPath = new string[1];
        string[] spritePath = new string[1];
        string spriteLocation;
        Vector2 colliderExtends;
        enemyPath[0] = "Assets/Objekte/Enemy";
        spritePath[0] = "Assets/Objekte/Enemy/";
        Debug.Log("Creating Enemies...");
        Debug.Log("0%\n");
        Object prefab;
        progress = 0;
        foreach (Dictionary<string, string> enemy in enemyDictionaries)
        {
            progress += 100 / enemyDictionaries.Length;
            Debug.Log(progress + "%\n");
            //Wenn das Dictionary des Feindes fehlerhaft ist überspringe ihn
            if (!isEnemyValid(enemy))
            {
                // print("enemy invalid. enemy skipped");
                continue;
            }
            hasOwnSprite = false;
            if(AssetDatabase.LoadAssetAtPath(enemyPath[0] + "/" + enemy["Name"] + ".prefab", typeof(Object)) == null){
            //Erstelle ein leeres Prefab mit dem Namen des Gegners im Asset/Prefab Ordner
                 prefab = PrefabUtility.CreateEmptyPrefab(enemyPath[0] + "/" + enemy["Name"] + ".prefab");
            }
            else
            {
                 continue;
                 //prefab = AssetDatabase.LoadAssetAtPath(enemyPath[0] + "/" + enemy["Name"] + ".prefab", typeof(Object));
            }
            //Erstelle das GameObject, dass als PreFab gespeichert werden soll

            //Lade das zugehörige Sprite und speichere es zwischen
            spriteLocation = spritePath[0] + enemy["Sprite"];
            if (AssetDatabase.LoadAssetAtPath(spriteLocation, typeof(Sprite)) != null)
            {
                enemySprite = AssetDatabase.LoadAssetAtPath(spriteLocation, typeof(Sprite));
                //   slicedSprite = Sprite.Create(enemySprite as Texture2D, new Rect(0,0,64,64),new Vector2(32,32), 64);
                //  hasOwnSprite = true;

            }
            else
            {
                spriteLocation = spritePath[0] + "Graphics_Enemy.png";
                enemySprite = AssetDatabase.LoadAssetAtPath(spriteLocation, typeof(Sprite));

            }

            //if (AssetDatabase.FindAssets(enemy["Name"],enemyPath) == null)
            //  {
            GameObject enemyPreFab = new GameObject();
            enemyPreFab.name = enemy["Name"];
            enemyPreFab.AddComponent<SpriteRenderer>();

            if (enemySprite != null && !hasOwnSprite)
            {
                enemyPreFab.GetComponent<SpriteRenderer>().sprite = enemySprite as Sprite;
            }
            /* else if (hasOwnSprite) 
             {
                 enemyPreFab.GetComponent<SpriteRenderer>().sprite = slicedSprite;
             }*/

            enemyPreFab.AddComponent<Rigidbody2D>();
            if (enemy["Collidersize"] != null)
                colliderExtends = new Vector2(float.Parse(enemy["Collidersize"]), float.Parse(enemy["Collidersize"]));
            else
                colliderExtends = new Vector2(0.5f, 0.5f);

            enemyPreFab.AddComponent<BoxCollider2D>();
            enemyPreFab.GetComponent<BoxCollider2D>().size = colliderExtends;

            if (enemy["Name"].Contains("_defense") || enemy["Name"].Contains("_Defense"))
            {
                enemyPreFab.AddComponent<EnemyController_Defense>();
                enemyPreFab.GetComponent<EnemyController_Defense>().setup(float.Parse(enemy["Speed"]), float.Parse(enemy["LifePoints"]), int.Parse(enemy["Resources"]));
            }
            else
            {
                //enemyPreFab.AddComponent<EnemyController>;
                //enemyPreFab.GetComponent<EnemyController>.setup(enemy["Speed"], enemy[LifePoints], enemy["Resources"]);
            }

            //Ersetze den Inhalt des PreFabs mit dem Inhalt des erstellten GameObjects
            PrefabUtility.ReplacePrefab(enemyPreFab, prefab, ReplacePrefabOptions.ConnectToPrefab);
            //Zerstöre das temporäre GameObject
            Destroy(enemyPreFab);
            // }
        }
        Debug.Log("100%");
    }


    private bool isEnemyValid(Dictionary<string, string> enemy)
    {

        if (enemy["Name"] == null ||
            enemy["Type"] == null ||
            enemy["Shoots"] == null ||
            enemy["Speed"] == null ||
            enemy["LifePoints"] == null ||
            enemy["Resources"] == null ||
            enemy["Sprite"] == null ||
            enemy["Collidersize"] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /*
     * 
     * 
     * */

    private Dictionary<string, string> loadLevel()
    {
        XmlDocument levelXml = new XmlDocument();
        levelXml.LoadXml(levelDoc.text);
        XmlNodeList levelNodes = levelXml.GetElementsByTagName("Level"); //Erstellt ein Array aus den einzelnen Levelnodes des XML
        Dictionary<string, string> levelInfo = new Dictionary<string, string>(); //Erstellt ein Array von Dictionaries, um jedem Feind ein neues geben zu können

        int nodeLevelNumber = 0;
        int spawnpointCounter = 0;
        XmlNodeList spawnNodes;
        string spawnpointInfo = "";
        foreach (XmlNode levelNode in levelNodes)
        {
            XmlNodeList nodeContent = levelNode.ChildNodes;
            foreach (XmlNode nodeItem in nodeContent)
            {
                //print(nodeItem.InnerText);
                if (nodeItem.Name == "Levelnumber")
                    nodeLevelNumber = int.Parse(nodeItem.InnerText);
                if (nodeLevelNumber == levelNumber)
                {
                    if (nodeItem.Name == "Levelnumber")
                        levelInfo.Add("Levelnumber", nodeItem.InnerText);

                    if (nodeItem.Name == "Mode")
                        levelInfo.Add("Mode", nodeItem.InnerText);

                    if (nodeItem.Name == "Difficulty")
                        levelInfo.Add("Difficulty", nodeItem.InnerText);

                    if (nodeItem.Name == "Enemytype_1")
                        levelInfo.Add("Enemytype_1", nodeItem.InnerText);

                    if (nodeItem.Name == "Enemytype_2")
                        levelInfo.Add("Enemytype_2", nodeItem.InnerText);

                    if (nodeItem.Name == "Enemytype_3")
                        levelInfo.Add("Enemytype_3", nodeItem.InnerText);

                    if (nodeItem.Name == "Enemytype_4")
                        levelInfo.Add("Enemytype_4", nodeItem.InnerText);

                    //Spawnpoints auslesen
                    if (nodeItem.Name == "Spawnpoint") 
                    {
                        spawnpointInfo = "";
                        spawnpointCounter++;
                        spawnNodes = nodeItem.ChildNodes;
                        foreach(XmlNode spawnNodeContent in spawnNodes)
                        {
                            if (spawnNodeContent.Name == "Spawnpoint_x")
                                spawnpointInfo += spawnNodeContent.InnerText + ",";

                            if (spawnNodeContent.Name == "Spawnpoint_y")
                                spawnpointInfo += spawnNodeContent.InnerText;
                        }

                        levelInfo.Add("Spawnpoint_" + spawnpointCounter.ToString(), spawnpointInfo);
                    }
                }
            }
        }
        return levelInfo;
    }


    public Dictionary<string, string> initialiseLevel(int currentLevel)
    {
        this.levelNumber = currentLevel;
        print("Levelnumber: " + levelNumber.ToString());
        Debug.Log("Initializing...\n");
        loadEnemies();
        createEnemyPreFabs();

        return loadLevel();
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}