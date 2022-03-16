using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Menu : MonoBehaviour
{
    public static Menu Instance; // making menu a singleton explained in the enemy script
    public TMP_Text Gamemode_Text; // getting a reference to the gamemode text in the main menu 
    public TMP_Text Score_Text; // getting a reference to the score text in the main menu
    [Header("Don't edit this variable or it will break a lot of things")]
    public string gamemode; // declaring a public string to hold the gamemode 
    public int highscore; // declaring a public int to hold the highscore

    private void Awake()
    {
        Instance = this; // setting instance equal to this class inside of this script
    }

    private void Start() // when the script starts
    {
        Sync(); // if there is a saved gamemode sync the gamemode text to the saved gamemode
        LoadSave(); // load the save file
        CheckAndSend(); // check for the game scene and if true send gamemode to the endmanager
        Init(); // calling the init function to set some default values if they haven't alredy been set
    }

    public void ToGame() // function that sends the player to the game scene
    {
        SceneManager.LoadScene("Game"); // loading the game scene
    }

    public void ChangeValue(bool value) // function change the gamemode depending on the value of the toggle in the menu scene
    {
        if (value) // if the value is true
        {
            gamemode = "levels"; // set the gamemmode to levels
            Gamemode_Text.text = "Levels"; // setting the gamemode text on the main menu to the gamemode
        }
        else if (!value) // else if the value is false
        {
            gamemode = "endless"; // set the gamemode to endless
            Gamemode_Text.text = "Endless"; // setting the gamemode text on the main menu to the gamemode
        }

        SaveSystem.SaveProfile(this); // after making a change save the profile / save
    }

    public void LoadSave() // function to check for a file and save the save
    {    
        string path = Application.persistentDataPath + "/save.lafe"; // declaring and defining the path to the save file

        if (File.Exists(path)) // if a file with the specified name exists in the specified path
        {
            SaveData data = SaveSystem.LoadProfile(); // create a new savedata 

            gamemode = data.gamemode; // set gamemode equal to the saved gamemode
            highscore = data.highscore; // set highscore equal to the saved highscore
        }
    }

    public void CheckAndSend() // function to check for the game scene and if true send the saved gamemode over
    {
        Scene currentScene = SceneManager.GetActiveScene(); // create a new local scene variable and setting it equal to the active scene
        string sceneName = currentScene.name; // creating a new local string and setting equal to the local scene variable's name

        if (sceneName == "Game") // if the sceneName is equal to Game then we are in the game scene therefore 
        {
            EndManager.Instance.ReceiveGamemode(gamemode); // send the saved gamemode over to the endmanager script
        }
    }

    private void Init() // function is called when the script starts ^^^^^^^
    {
        if (gamemode != "levels" && gamemode != "endless") // checking if the gamemode is null / has not been set to anything yet 
        {
            gamemode = "levels"; // setting the gamemode to levels
            Gamemode_Text.text = "Levels"; // setting the gamemode text on the main menu to the gamemode
            SaveSystem.SaveProfile(this); // after making a change save the profile / save
        }
    }

    private void Sync() // function to sync the main menu gamemode text when the script starts 
    {
        if (string.IsNullOrEmpty(gamemode) == false) // if gamemode is not null and is not empty
        {
            if (gamemode == "levels") // if the gamemode is set to levels
            {
                Gamemode_Text.text = "Levels"; // setting the gamemode text on the main menu to the gamemode
            }
            else if (gamemode == "endless") // else if the gamemode is set to endless
            {
                Gamemode_Text.text = "Endless"; // setting the gamemode text on the main menu to the gamemode
            }
        }

        StartCoroutine(SetScore()); // start the setscore coroutine to update the high score display text      
    }

    private IEnumerator SetScore() // coroutine to update the high score
    {
        Scene currentScene = SceneManager.GetActiveScene(); // declaring and defining a local scene variable to the active scene
        string sceneName = currentScene.name; // declaring and defining a local string to the var above's name ^

        if (sceneName == "Menu") // if the active scene is the menu 
        {
            yield return new WaitForSeconds(0.1f); // wait .1 seconds
            Score_Text.text = highscore.ToString(); // update the score text to the saved highscore
        }    
    }

    public void DeleteSave() // function to delete a save | ONLY USE FOR TESTING PURPOSES SAVES ARE NOT RETRIEVABLE
    {
        SaveSystem.DeleteProfile(); // deletes the save file
        Debug.Log("<color=red>Save File Deleted</color>");
    }

    public void UpdateScore(int __score) // function to update the high score
    {
        SaveData data = SaveSystem.LoadProfile(); // create a new savedata 

        highscore = data.highscore; // setting highscore equal to the saved highscore

        if (highscore != 0) // if highscore is not equal to 0
        {
            if (__score > highscore) // if the score we passed in is greater than the saved highscore
            {
                highscore = __score; // set the highscore to the score
            }
        }
        else 
        {
            highscore = __score; // set the highscore to the score
        }

        SaveSystem.SaveProfile(this); // save the save
    }
}
