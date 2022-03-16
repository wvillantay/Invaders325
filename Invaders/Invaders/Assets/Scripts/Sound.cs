using System.Collections;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // making this class a singleton | explained in the enemy script
    public static Sound Instance;
    // public gameobject to get a reference to the SFX_Shoot prefab
    public GameObject Sound_Shoot;
    // public gameobject to get a reference to the SFX_Explosion prefab
    public GameObject Sound_Explosion;
    // private int to keep track of how many sounds are in the scene 
    private int sounds;

    // when the script starts
    private void Awake()
    {
        // instance is equal to this class
        Instance = this;
    }

    // function to play a sound, takes in a string to identify the sound
    public void PlaySound(string sound)
    {
        // if the passed in string is Shoot
        if (sound == "Shoot")
        {
            // create a new local gameobject equal to the new shoot sfx we just created
            GameObject SFX_Shoot = Instantiate(Sound_Shoot);
            // start the coroutine to cull this sound
            StartCoroutine(CullSound(SFX_Shoot));

            // add 1 to the sounds in the scene
            sounds++;
            // rename the local gameobject to it's respective sound + the count of sounds in the scene
            SFX_Shoot.name = "SFX_Shoot_" + sounds;
        }
        else if (sound == "Explosion") // if the passed in string is Explosion
        {
            // create a new local gameobject equal to the new explosion sfx we just created
            GameObject SFX_Explosion = Instantiate(Sound_Explosion);
            // start the coroutine to cull this sound
            StartCoroutine(CullSound(SFX_Explosion));

            // add 1 to the sounds in the scene
            sounds++;
            // rename the local gameobject to it's respective sound + the count of sounds in the scene
            SFX_Explosion.name = "SFX_Explosion_" + sounds;
        }
    }

    // coroutine to cull the passed in gameobject
    private IEnumerator CullSound(GameObject instance)
    {
        // wait 1 1/2 seconds
        yield return new WaitForSeconds(1.5f);
        // destroy the passed in gameobject (sound)
        Destroy(instance);
        // remove 1 from the sounds in the scene int
        sounds--;
    }
}
