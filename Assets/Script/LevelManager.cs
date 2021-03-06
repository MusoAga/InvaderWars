﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ONLY FOR GAMEMENU
using UnityEngine.EventSystems;


public class LevelManager : MonoBehaviour {

    public Transform mainMenu, highscoreMenu, optionsMenu, mitwirkendeMenu, upgradeMenu, planetMenu;

    public Text PlanetInfo;
    public Text PlanetName;
    public Text UpgradeInfo;
    public Text UpgradeName;

	public GameObject icePlanet;
	public GameObject swampPlanet;
	public GameObject volcanoPlanet;

	public GameObject lifepoints;
	public GameObject fireRate;
	public GameObject speed;
	public GameObject frostresistenz;
	public GameObject laser;


    private GameObject button;
	//private bool planetSchema = false;

    void Start()
    {
        if(Application.loadedLevelName.Equals("Planets"))
        {
            upgradeMenu.gameObject.SetActive(false);
            planetMenu.gameObject.SetActive(true);

			//planetSchema = true;
        }

		//test = GameObject.Find ("SchwampPlanet");
    }

    void Update()
    {
        if(FindObjectOfType<GameController>() != null && FindObjectOfType<GameController>().isLevelComplete())
        {
            FindObjectOfType<GameController>().unlockNextLevel();
            FindObjectOfType<GameController>().setLevelComplete(false);
        }


		//Infotexte der Planeten sollen schon bei einem ausgewählten Button(Planeten) gezeigt werden
		if (icePlanet == EventSystem.current.currentSelectedGameObject) {

			PlanetInfo.text = "Schnee, Eis und die Kälte.\nDie Umgebung macht das Leben\nfür die Raumschiffe schwer.";
			PlanetName.text = "Iconom";
			FindObjectOfType<GameController>().setCurrentLevel(1);
		}
		else if (swampPlanet == EventSystem.current.currentSelectedGameObject)
		{
			PlanetInfo.text = "Gift";
			PlanetName.text = "Gifti";
			FindObjectOfType<GameController>().setCurrentLevel(2);
		}
		else if (volcanoPlanet == EventSystem.current.currentSelectedGameObject)
		{
			PlanetInfo.text = "FEUER!";
			PlanetName.text = "Feuri";
			FindObjectOfType<GameController>().setCurrentLevel(3);
		}

		//Infotexte der Updates
		if (lifepoints == EventSystem.current.currentSelectedGameObject)
		{
			UpgradeInfo.text = "Upgrade your armor to \nwithstand your enemies!";
			UpgradeName.text = "Increase  Armor: +1";
		}
		else if (fireRate == EventSystem.current.currentSelectedGameObject)
		{
			UpgradeInfo.text = "With this upgrade, you can\n shoot your enemies faster!";
			UpgradeName.text = "Increase  Firerate: +1";
		}
		else if (speed == EventSystem.current.currentSelectedGameObject)
		{
			UpgradeInfo.text = "With this you can dodge\n the shoots very skillfully!";
			UpgradeName.text = "Increase  Speed: +1";
		}
		else if (frostresistenz == EventSystem.current.currentSelectedGameObject)
		{
			UpgradeInfo.text = "Don't let your enemies slow you down!";
			UpgradeName.text = "Increase Frost Resistance: +1";
		}
		else if (laser == EventSystem.current.currentSelectedGameObject)
		{
			UpgradeInfo.text = "Piiow  Piiow!";
			UpgradeName.text = "Unbelievable  Laser  Shot";
		}
	
    }

    public void StartGame(string name)
    {
        Application.LoadLevel(name);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }

    public void OptionsMenu(bool clicked)
    {
        if(clicked)
        {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);


        } else
        {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void MitwirkendeMenu(bool clicked)
    {
        if (clicked)
        {
            optionsMenu.gameObject.SetActive(clicked);
            mitwirkendeMenu.gameObject.SetActive(false);
        }
        else
        {
            optionsMenu.gameObject.SetActive(clicked);
            mitwirkendeMenu.gameObject.SetActive(true);
        }
    }

    public void HighscoreMenu(bool clicked)
    {
        if (clicked)
        {
            highscoreMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            highscoreMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void UpgradeMenu(bool clicked)
    {
        if (clicked)
        {
            upgradeMenu.gameObject.SetActive(clicked);
            planetMenu.gameObject.SetActive(false);
        }
        else
        {
            upgradeMenu.gameObject.SetActive(clicked);
            planetMenu.gameObject.SetActive(true);
        }
    }



    /*
    FÜR DIE PLANETENÜBERSICHT!
    */
	//TODO Kann weg
    public void UpdatePlanetInfo(string planetName)
    {
        if (planetName.Equals("IcePlanet"))
        {
            PlanetInfo.text = "Schnee, Eis und die Kälte.\nDie Umgebung macht das Leben\nfür die Raumschiffe schwer.";
            PlanetName.text = "Iconom";
            FindObjectOfType<GameController>().setCurrentLevel(1);
        }
        else if (planetName.Equals("SwampPlanet"))
        {
            PlanetInfo.text = "Gift";
            PlanetName.text = "Gifti";
            FindObjectOfType<GameController>().setCurrentLevel(2);
        }
        else if (planetName.Equals("VolcanoPlanet"))
        {
            PlanetInfo.text = "FEUER!";
            PlanetName.text = "Feuri";
            FindObjectOfType<GameController>().setCurrentLevel(3);
        }

    }

    /*
    FÜR DIE UPGRADES!
    */
	//TODO Kann weg
    public void UpdateUpgradeInfo(string upgradeName)
    {

        if (upgradeName.Equals("Lifepoints"))
        {
            UpgradeInfo.text = "Upgrade your armor to \nwithstand your enemies!";
            UpgradeName.text = "Increase  Armor: +1";
        }
        else if (upgradeName.Equals("Firerate"))
        {
            UpgradeInfo.text = "With this upgrade, you can\n shoot your enemies faster!";
            UpgradeName.text = "Increase  Firerate: +1";
        }
        else if (upgradeName.Equals("Speed"))
        {
            UpgradeInfo.text = "With this you can dodge\n the shoots very skillfully!";
            UpgradeName.text = "Increase  Speed: +1";
        }
        else if (upgradeName.Equals("Frostresistenz"))
        {
            UpgradeInfo.text = "Don't let your enemies slow you down!";
            UpgradeName.text = "Increase Frost Resistance: +1";
        }
        else if (upgradeName.Equals("Schusswechsel"))
        {
            UpgradeInfo.text = "Piiow  Piiow!";
            UpgradeName.text = "Unbelievable  Laser  Shot";
        }

    }

    public void StartPlanetLevel()
    {
        FindObjectOfType<GameController>().StartPlanetLevel();
    }

    public void upgradePlayer(string upgradeName)
    {
        if(Input.GetMouseButtonUp(0))
            FindObjectOfType<GameController>().upgradePlayer(upgradeName);
    }
		
		


}
