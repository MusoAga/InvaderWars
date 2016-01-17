using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ONLY FOR GAMEMENU
public class LevelManager : MonoBehaviour {

    public Transform mainMenu, optionsMenu, mitwirkendeMenu, upgradeMenu, planetMenu;

    public Text PlanetInfo;
    public Text PlanetName;
    public Text UpgradeInfo;
    public Text UpgradeName;

    private string upgradeName;

    private bool upgrade = false;

    private GameObject button;

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
    public void UpdateUpgradeInfo(string upgradeName)
    {
        upgrade = true;
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

        this.upgradeName = upgradeName;

    }

    public void StartPlanetLevel()
    {
        FindObjectOfType<GameController>().StartPlanetLevel();
    }

    public void upgradePlayer()
    {
        if(upgrade)
        {
            FindObjectOfType<GameController>().upgradePlayer(upgradeName);
            upgrade = false;
        }
    }
}
