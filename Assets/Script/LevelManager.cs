using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ONLY FOR GAMEMENU
public class LevelManager : MonoBehaviour {

    public Transform mainMenu, optionsMenu, mitwirkendeMenu;

    public Text PlanetInfo;
    public Text PlanetName;

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

    public void StartPlanetLevel()
    {
        FindObjectOfType<GameController>().StartPlanetLevel();
    }
}
