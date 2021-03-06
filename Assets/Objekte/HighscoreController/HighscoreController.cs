﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighscoreController : MonoBehaviour {

    private int entryPoints;
    private string entryName;

    private SortedList<int, string> highscoreList;

    public void addEntry(string name, int points)
    {
        if(highscoreList != null) {
            if (!highscoreList.ContainsKey(points))
            {
                highscoreList.Add(points, name);
                print("Add highscore entry: " + name + " | " + points.ToString());
            }
        }
        else {
            highscoreList = new SortedList<int, string>();
            this.addEntry(name, points);
        }
    }

    public SortedList<int, string> getHighscoreList()
    {
        return highscoreList;
    }

}
