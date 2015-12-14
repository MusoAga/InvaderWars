using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public Transform mainMenu, optionsMenu, gameBeforStart;

    public void StartGame(string name)
    {
        Application.LoadLevel(name);
    }

    public void EndGame()
    {
        Application.Quit();
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

    public void EnterNameMenu(bool clicked)
    {
        if (clicked)
        {
            gameBeforStart.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        } else
        {
            gameBeforStart.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
}
