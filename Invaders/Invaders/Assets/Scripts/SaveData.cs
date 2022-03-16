using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string gamemode;
    public int highscore;

    public SaveData(Menu menu)
    {
        gamemode = menu.gamemode;
        highscore = menu.highscore;
    }
}