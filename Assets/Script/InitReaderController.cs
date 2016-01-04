using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

public class InitReaderController : MonoBehaviour {

    private int levelNumber = 0;
    public TextAsset enemyDoc;
    public TextAsset levelDoc;
    public TextAsset playerDoc;
    Dictionary<string, string>[] enemyDictionaries;


    private void printEnemyStats(Dictionary<string, string> enemyDic)
    {
        print("Enemy: " + enemyDic["Name"]);
        foreach (string stat in enemyDic.Keys)
        {
            print("\nEnemystat: " + stat + " Value: " + enemyDic[stat]);
        }
    }


    /*
     * Läd die Werte aller Feinde aus dem Enemy.Xml und speichert sie pro Feind in einem Dictionary
     * 
     * */
    private void loadEnemies()
    {
        XmlDocument enemyXml = new XmlDocument();
        enemyXml.LoadXml(enemyDoc.text);
        XmlNodeList enemyNodes = enemyXml.GetElementsByTagName("Enemy"); //Erstellt ein Array aus den einzelnen Levelnodes des XML
        enemyDictionaries = new Dictionary<string, string>[enemyNodes.Count]; //Erstellt ein Array von Dictionaries, um jedem Feind ein neues geben zu können
       // print("Enemynodes: " + enemyNodes.Count);
       // print("Enemyarray: " + enemyDictionaries.Length);
        int i = 0;
        foreach (XmlNode enemyNode in enemyNodes) {
            XmlNodeList nodeContent = enemyNode.ChildNodes;
            print("Contentnodes pro EnemyNode: " + nodeContent.Count);
            enemyDictionaries[i] = new Dictionary<string, string>();
            if (enemyDictionaries[i] != null)
                enemyDictionaries[i].Clear();

            foreach(XmlNode nodeItem in nodeContent){
              {

                  if (enemyDictionaries == null)
                      print("Enemydic ist leer!");
                  else
                      print("EnemyDic existiert");

                  if(nodeItem.Name == "Name")
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
              }

            }
            if(i < enemyDictionaries.Length)
            i++;
        }
        foreach (Dictionary<string, string> dic in enemyDictionaries)
        {
            print("\n");
            printEnemyStats(dic);
        }
    }//End loadEnemies()


    /*
     * Erzeugt bzw. updatet die Gegnerprefabs
     * 
     * 
     * */

    private void createEnemyPreFabs()
    {
        string[] path = new string[2];
        path[0] = "Assets/Prefab";
        path[1] = "Assets/Sprite";
        foreach (Dictionary<string, string> enemy in enemyDictionaries)
        {
            if (AssetDatabase.FindAssets(enemy["Name"],path) == null)
            {
                GameObject enemyPreFab = new GameObject();
                enemyPreFab.AddComponent<SpriteRenderer>();
              //  enemyPreFab.GetComponent<SpriteRenderer>().sprite = AssetDatabase.FindAssets("texture_test", path);
            }
          //  GameObject enemyPreFab = new GameObject();
          //  enemyPreFab.name = enemy["Name"];

        }
    }


    /*
     * 
     * 
     * */

    private void loadLevel() 
    {
        XmlDocument levelXml = new XmlDocument();
        levelXml.LoadXml(levelDoc.text);
        XmlNodeList levelNodes = levelXml.GetElementsByTagName("Level"); //Erstellt ein Array aus den einzelnen Levelnodes des XML
       

        foreach (XmlNode levelNode in levelNodes) {
            XmlNodeList nodeContent = levelNode.ChildNodes;
            foreach(XmlNode nodeItem in nodeContent){
              if (levelNode.Name == levelNumber.ToString()) //Sucht die Werte des angegebenen Levels
              {
                 XmlNodeList levelContent = levelNode.ChildNodes;
                    nodeItem.ToString(); // Kann gelöscht werden
                    levelContent.ToString(); // Kann gelöscht werden

                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        loadEnemies();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
