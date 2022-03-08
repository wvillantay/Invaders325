using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    // declaring a private bool to determine whether the ufo should be moving or not 
    private bool shouldmove;

    // when the script starts
    private void Start()
    {
        // run the moveufobridge function in 10 seconds and then every 15 seconds after that
        InvokeRepeating("MoveUFOBridge", 10f, 15f);
    }

    // every frame
    private void Update()
    {
        // if the ufo should move
        if (shouldmove)
        {
            // translate it's x position by 0.012f units every frame
            transform.Translate(0.005f, 0, 0);
        }
    }

    // starts the moveufo coroutine through a function since you can't invoke a coroutine
    private void MoveUFOBridge()
    {
        // starting the moveufo coroutine
        StartCoroutine(MoveUFO());
    }

    // coroutine that sets should move to true waits and then to false and resets it's position
    private IEnumerator MoveUFO()
    {
        // setting should move to true
        shouldmove = true;
        // waiting 2 and a half seconds
        yield return new WaitForSeconds(2.5f);
        // setting should move to false
        shouldmove = false;

        // resetting the ufo's position
        transform.position = new Vector3(-10, 3, 0);
    }

    // when something collides with the ufo
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if what collided with the ufo was a bullet
        if (collision.gameObject.tag == "Bullet")
        {
            // setting the bullet that hit the ufo to inactive
            collision.gameObject.SetActive(false);

            // adding 200 points to the score
            Score.Instance.Add(200);

            // setting shouldmove to false so the ufo stops moving
            shouldmove = false;
            // resetting the ufo's position
            transform.position = new Vector3(-10, 3, 0);
        }
    }
}
