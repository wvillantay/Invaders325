using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(BackToMain()); // start the coroutine that takes us to the main menu
    }

    private IEnumerator BackToMain() 
    {
        yield return new WaitForSeconds(5f); // wait 5 seconds
        SceneManager.LoadScene(0); // load the scene at (main menu)
    }
}
