using UnityEngine;
using UnityEngine.SceneManagement;

public class Line : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
