using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; // making this class a singleton explained in the enemy script
    private int level; // declaring a private int to keep track of what level the player is on
    public GameObject Player; // declaring a public gameobject to get a reference to the player prefab so that we can instantiate it later on
    public GameObject Enemies_2; // declaring a public gameobject to get a reference to the second round of enemies so that we can instantiate it later on
    public GameObject Enemies_3; // declaring a public gameobject to get a reference to the third round of enemies so that we can instantiate it later on

    private void Awake() // before this script starts
    {
        Instance = this; // setting Instance equal to this class inside of this script
    }

    public void CheckLevel(int _level) // function which takes in an int and determines whether to start a new level or not
    {
        if (level == 1) // if the player has completed the first level 
        {
            EndManager.Instance.CloseEndScreen();  // close the end screen
            GameObject _player = Instantiate(Player); // instantiating the player prefab under a local gameobject
            _player.name = "Player"; // changing the prefabs name to Player

            GameObject _enemies2 = Instantiate(Enemies_2); // instantiating the second round of enemies under a local gameobject
            _enemies2.name = "Enemies_2"; // changing the prefabs name to Enemies_2
        }
        else if (level == 2) // else if the player has completed the second level 
        {
            EndManager.Instance.CloseEndScreen();  // close the end screen
            GameObject _player = Instantiate(Player); // instantiating the player prefab under a local gameobject
            _player.name = "Player"; // changing the prefabs name to Player

            GameObject _enemies3 = Instantiate(Enemies_3); // instantiating the third round of enemies under a local gameobject
            _enemies3.name = "Enemies_3"; // changing the prefabs name to Enemies_3
        }
    }
}
