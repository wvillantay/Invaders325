using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public static EndManager Instance; // making this class a singleton explained in the enemy script before
    private int enemycount; // declaring a private int to keep track of how many enemies are in the level
    private bool ready; // declaring a private bool which prevents the update function from causing a win state before the enemies have been counted
    public GameObject EndScreen; // declaring a public gameobject to get a reference to the endscreen
    public TMP_Text Status; // declaring a public tmp text component to get a reference to the status text in the endscreen
    public TMP_Text _Score; // declaring a public tmp text component to get a reference to the score text in the endscreen
    private string win = "You Win"; // declaring a private string for ease of access 
    private string lose = "Game Over"; // declaring a private string for ease of access 
    private string gamemode; // declaring a private string to determine what gamemode the player is playing (endless or levels)
    private int level; // declaring a private int to determine what level the player is on
    public GameObject Enemies_1; // declaring a public gameobject to get a reference to the first round of enemies so that we can instantiate it later on
    public GameObject Enemies_2; // declaring a public gameobject to get a reference to the second round of enemies so that we can instantiate it later on
    public GameObject Enemies_3; // declaring a public gameobject to get a reference to the third round of enemies so that we can instantiate it later on


    private void Awake() 
    {
        Instance = this; 
    }

    private void Start() 
    {
        StartCoroutine(Ready()); // starting the ready coroutine
        level = 1; 
    }

    public void Add() // each instance of the enemy script will call this function therefore giving us the amount of enemies in the level
    {
        enemycount++; // incrementing the count of enemies in the level
    }

    public void Remove() // when an enemy dies they will call this function
    {
        enemycount--; 
    }

    private void Update() // called every frame
    {

        if (enemycount <= 0 && ready) // if there are less than or equal to 0 enemies in the level and ready is true
        {
            if (gamemode == "levels") // if the player is playing the levels gamemode
            {
                Score.Instance.SendScore(); // sending score
                EndScreen.SetActive(true); // setting the endscreen to active
                Status.text = win; // setting the status text to the win state
            }
            else if (gamemode == "endless") 
            {
                ready = false; // set ready equal to false
                StartCoroutine(Ready()); // start the ready

                int random; 

                random = Random.Range(1, 4); // set random to a random int between 1 and 3 (last number is excluded)
                switch(random) // choose a different outcome depending on the value of random
                {
                    case 1: // if random is 1
                        Instantiate(Enemies_1); // instantiate the first wave of enemies
                        break;
                    case 2: // if random is 2
                        Instantiate(Enemies_2); // instantiate the second wave of enemies
                        break;
                    case 3: // if random is 3
                        Instantiate(Enemies_3); // instantiate the third wave of enemies 
                        break;
                }
            }
        }
        
    }

    private IEnumerator Ready() // coroutine to set ready to true after a set time
    {
        yield return new WaitForSeconds(1f); // wait 1 second
        ready = true; // setting ready to true
    }

    public void Lose() // function which will be called when the player dies
    {
        Score.Instance.SendScore(); // sending score
        EndScreen.SetActive(true); 
        Status.text = lose; 
        ready = false; // setting ready to false so that we don't check for the amount of enemies anymore
    }

    public void Quit() // call this back button in the endscreen
    {
        SceneManager.LoadScene("Menu"); 
    }

    public void Continue() 
    {
        if (gamemode == "endless") // if the player is playing the endless gamemode
        {
            Quit(); // the gamemode is endless so if we are in the endscreen we must of died
        }
        else if (gamemode == "levels") // if the player is playing the levels gamemode
        {
            Debug.Log($"<color=pink>{level}</color>");
            if (level == 1) // if level is equal to 1 
            {
                Destroy(GameObject.Find("Enemies")); // destroying the old enemies

                Debug.Log("<color=green>Level 1 Detected</color>");
                CloseEndScreen(); // get the game ready
                Instantiate(Enemies_2); // instantiate the new enemies 
                level++; // increment the level count

                Enemy.Instance.IncreaseSpeed(); // increasing the enemies speed
            }
            else if (level == 2) // if level is equal to 2 
            {
                Destroy(GameObject.Find("Enemies_2")); 

                Debug.Log("<color=yellow>Level 2 Detected</color>");
                CloseEndScreen(); // get the game ready
                Instantiate(Enemies_3); // instantiate the new enemies
                level++; // increment the level count

                Enemy.Instance.IncreaseSpeed(); // increasing the enemies speed
            }
            else if (level == 3) // if level is equal to 3 
            {
                Debug.Log("<color=orange>Level 3 Detected</color>");
                Quit(); // go back to the main menu since we have completed the final level
            }
        }
    }

    public void ReceiveGamemode(string _gamemode) // function to receive the saved gamemode and then update it here
    {
        gamemode = _gamemode; // setting gamemode equal to the saved gamemode 
    }

    public void ReceiveScore(int score) // function to receive the score that the Score script sends over
    {
        _Score.text = $"score <<color=#00FF0B>{score}</color>>"; // setting the scores text to the passed in int converted to a string 
        Menu.Instance.UpdateScore(score);
    }

    public void CloseEndScreen() // function to close the end screen and return to the game
    {
        EndScreen.SetActive(false); // setting the endscreen to inactive
        ready = false; // setting ready to false
        StartCoroutine(Ready()); // starting the ready coroutine to start the detection process
    }
}