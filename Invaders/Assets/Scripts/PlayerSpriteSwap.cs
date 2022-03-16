using System.Collections;
using UnityEngine;

public class PlayerSpriteSwap : MonoBehaviour
{
    public static PlayerSpriteSwap Instance; // making this class a singleton explained in enemy script
    public GameObject Player_Main_State; // referencing the player's main sprite
    public GameObject Player_Animation_State_1; // referencing all of the player's animation states
    public GameObject Player_Animation_State_2;// 
    public GameObject Player_Animation_State_3;//
    public GameObject Player_Animation_State_4;//

    private void Awake() // before this script is started
    {
        Instance = this; 
    }

    private void Start() // when this script starts
    {
        Init(); // call this initialise function 
    }

    private void Init() // function to initialise the script
    {
        Player_Main_State.SetActive(true); // enable the main sprite
        Player_Animation_State_1.SetActive(false); // disable all the other animation sprites
        Player_Animation_State_2.SetActive(false);//
        Player_Animation_State_3.SetActive(false);//
        Player_Animation_State_4.SetActive(false);//
    }

    public void Trigger() // function to trigger the explode coroutine
    {
        StartCoroutine(Explode()); // calling the explode coroutine
    }

    private IEnumerator Explode() // coroutine to cycle through the explosion animation states
    {
        Player_Main_State.SetActive(false); // disable the main sprite
        Player_Animation_State_1.SetActive(true); // enable first animation sprite
        yield return new WaitForSeconds(0.1f); // wait 0.1 seconds
        Player_Animation_State_1.SetActive(false); // disable first animation sprite
        Player_Animation_State_2.SetActive(true); // enable second animation sprite
        yield return new WaitForSeconds(0.1f); // wait 0.1 seconds
        Player_Animation_State_2.SetActive(false); // disable second animation sprite
        Player_Animation_State_3.SetActive(true); // enable third animations sprite
        yield return new WaitForSeconds(0.1f); // wait 0.1 seconds
        Player_Animation_State_3.SetActive(false); // disable third animation sprite
        Player_Animation_State_4.SetActive(false); // enable fourth animation sprite
        yield return new WaitForSeconds(0.25f);
        Init();
        StopCoroutine(Explode());
    }
}
