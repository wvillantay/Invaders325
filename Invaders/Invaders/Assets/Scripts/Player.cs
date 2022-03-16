using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    // declaring a new rigidbody2d called rb
    private Rigidbody2D rb;
    // declaring a gameobject called bullet which is public therefore we can get a reference to it just by dragging our bullet prefab into the field in the inspector
    public GameObject bullet;
    // declaring a private boolean to determine whether or not the player is on cooldown from shooting
    private bool cooldown;
    // declaring a private integer to see how many bullets are in the scene at one time
    private int bullets;
    // declaring a private int to keep track of how many lives the player has
    private int lives;
    [Header("Player's Lives")]
    // getting references to all of the players lifes
    public GameObject life_1;
    public GameObject life_2;
    public GameObject life_3;
    public TMP_Text life_count;

    // when the script starts up
    private void Start()
    {
        // set the rigidbody2d we declared equal to the rigidbody2d component on the player
        rb = GetComponent<Rigidbody2D>();
        // setting lives equal to 3
        lives = 3;
        life_count.text = $"{lives}";
    }

    // every frame
    private void Update()
    {
        // creating a boolean variable which will be true when we press the up arrow and false otherwise
        bool shoot = Input.GetKey(KeyCode.Space);

        // if we click up arrow
        if (shoot)
        {
            // run the shoot function
            Shoot();
        }

        // calling the movement function
        Movement();
    }

    private void Movement()
    {
        // same as the shoot boolean above but for the left and right arrow keys 
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        // if we are pressing the left arrow key
        if (left)
        {
            // add 1.5 units of force to the players left direction | right * -1 = left. the * 1.5f at the end is the units of force we are applying 
            rb.AddForce(transform.right * -1 * 1.5f);
        }
        
        if (right)
        {
            // add 1.5 units of force to the players right direction 
            rb.AddForce(transform.right * 1.5f);
        }    
    }

    // creating a new coroutine which will wait 1/4 of a second and then remove the cooldown
    private IEnumerator Cooldown()
    {
        // adding the cooldown
        cooldown = true;
        // waiting 1/4 of a second
        yield return new WaitForSeconds(0.25f);
        // removing the cooldown
        cooldown = false;
    }

    private void Shoot()
    {
        // if we are not on cooldown
        if (!cooldown)
        {
            // start the cooldown
            StartCoroutine(Cooldown());
            // creating a new local gameobject called _bullet which is equal to the bullet we just created 
            GameObject _bullet = Instantiate(bullet);
            // setting the bullets position to our players position
            _bullet.transform.position = transform.position;
            // adding 1 to the bullets int
            bullets++;
            // setting the bullets name to Bullet_(how many bullets are in the scene)
            _bullet.name = "Bullet_" + bullets;

            // passing the bullet we just created into the cullbullets coroutine
            StartCoroutine(CullBullets(_bullet));

            // play the shoot sound
            Sound.Instance.PlaySound("Shoot");
        }
    }

    // creating a coroutine to remove the bullet after it's been in the scene for 3 seconds for good performance
    private IEnumerator CullBullets(GameObject instance)
    {
        // waiting 3 seconds
        yield return new WaitForSeconds(3);
        // destroying the gameobject that we passed in | L:79
        Destroy(instance);
        // removing 1 from the bullets in the scene
        bullets--;
    }

    // when something collides with the player
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if what collided with the player was an enemy bullet
        if (collision.gameObject.tag == "EnemyBullet")
        {
            // depending on the amount of lives the player has choose a different outcome
            if (lives == 3)
            {
                // remove 1 from lives
                lives--;
                // set the third life to inactive
                life_3.SetActive(false);
                // updating the life count text
                life_count.text = $"{lives}";
                
            }
            else if (lives == 2)
            {
                // remove 1 from lives
                lives--;
                // set the second life to inactive
                life_2.SetActive(false);
                // updating the life count text
                life_count.text = $"{lives}";
                
            }
            else if (lives == 1)
            {
                // remove 1 from lives
                lives--;
                // set the first life to inactive
                life_1.SetActive(false);
                // updating the life count text
                life_count.text = $"{lives}";
            }

            // if lives if less than or equal to 0
            if (lives <= 0)
            {
                // updating the life count text
                life_count.text = $"{lives}";
                SceneManager.LoadScene("Menu");
            }

            // play the explosion sound
            Sound.Instance.PlaySound("Explosion");
        }
    }

    private IEnumerator Finish()
    {
        // waiting 1 second
        yield return new WaitForSeconds(1);
        EndManager.Instance.Lose(); // calling the lose function in the endmanager                                    
    }
}
