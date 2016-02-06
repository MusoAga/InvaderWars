using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighscoreController : MonoBehaviour {

    private int entryPoints;
    private string entryName;

    private SortedList<string, int> highscoreList;

    public void addEntry(string name, int points)
    {
        if(highscoreList != null) {
            highscoreList.Add(name, points);
            print("Add highscore entry: " + name + " | " + points.ToString());
        }
        else {
            highscoreList = new SortedList<string, int>();
            this.addEntry(name, points);
        }
    }

    public SortedList<string, int> getHighscoreList()
    {
        return highscoreList;
    }

}
