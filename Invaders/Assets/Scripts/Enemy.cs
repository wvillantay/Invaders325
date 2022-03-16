using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // making this class a singleton meaning that we can call it from other classes without a reference to it
    public static Enemy Instance;
    // declaring a private integer for the health of this enemy 
    private int health;
    // public int so we can set what type of enemy this is so we can assign different amounts of score if its a higher level enemy
    public int type;
    // declaring a private bool to determine whether the enemy is on cooldown or not
    private bool cooldown;
    // getting a reference to the bullet
    public GameObject bullet;
    // declaring a private integer to keep track of how many bullets are in the scene
    private int bullets;
    // declaring a public layermask to specify what layer the player is on
    public LayerMask player;
    // declaring a private bool to determine whether there is another enemy infront of this one or not 
    private bool enemy_infront;
    // declaring a pulbic layermask to specifiy what layer the enemy is on
    public LayerMask enemy;
    // private integer to determine where the enemy is on the x axis
    private int sortingorder_x;
    // private integer to determine where the enemy is on the y axis
    private int sortingorder_y;
    // declaring a private bool to determine which way the enemy should move
    private bool _switch;
    // declaring a private float to hold the speed's value
    private float speed = 0.25f;
    // declaring a public gameobject to get a reference to the enemies main sprite
    public GameObject main_sprite;
    // declaring a public gameobject to get a reference to the enemies animated sprite
    public GameObject animated_sprite;
    // declaring a private int to determine whether it's the first sprite swap or not
    private int sprite_index; 

    // before the script starts
    private void Awake()
    {
        // setting instance equal to this class
        Instance = this;
    }

    // when the script starts
    private void Start()
    {
        // set health to 10
        health = 10;

        // starting the move enemy coroutine
        StartCoroutine(MoveEnemy());
        // starting the swap sprite coroutine
        StartCoroutine(SwapSprite());

        EndManager.Instance.Add(); // adding 1 to the count of enemies in the level 
    }

    // when something collides with this enemy
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if what collided with this enemy was a bullet
        if (collision.gameObject.tag == "Bullet")
        {
            // setting the bullet that hit this enemy to inactive
            collision.gameObject.SetActive(false);

            // choosing a different outcome depending on the value of type 
            switch(type)
            {
                case 1:
                    // adding 10 points to the score
                    Score.Instance.Add(10);
                    break;
                case 2:
                    // adding 20 points to the score
                    Score.Instance.Add(20);
                    break;
                case 3:
                    // adding 30 points to the score
                    Score.Instance.Add(30);
                    break;
                case 4:
                    // adding 200 points to the score
                    Score.Instance.Add(200);
                    break;
            }

            // removing 10 health from this enemy
            Remove(10);
        }
    }

    // creating a function that takes in an integer and removes the passed in integer from the health
    public void Remove(int amount)
    {
        // setting health equal to health minus amount
        health -= amount;

        // if the health is less than zero
        if (health <= 0)
        {
            // setting the health to zero
            health = 0;

            EndManager.Instance.Remove(); // removing 1 from the count of enemies in the level

            // play the explosion sound
            Sound.Instance.PlaySound("Explosion");

            // destroying this enemy and removing it from the scene
            Destroy(transform.gameObject);
        }
    }

    // every frame
    private void FixedUpdate()
    {
        // create a new raycast that shoots a raycast from the enemy downwards that only can hit the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, player);
        // create a new raycast that shoots a raycast from the enemy downwards that only can hit another enemy
        RaycastHit2D enemyhit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), -Vector2.up, Mathf.Infinity, enemy);

        // if an enemy is it
        if (enemyhit.collider != null)
        {
            // set enemy_infront to true
            enemy_infront = true;
        }
        else if (enemyhit.collider == null) // else if it doesn't hit another enemy
        {
            // set enemy_infront to false
            enemy_infront = false;
        }

        // if it hit a player
        if (hit.collider != null)
        {
            // if there isn't an enemy infront of this enemy
            if (!enemy_infront)
            {
                // try to shoot
                Shoot();
            }
        }
        
    }

    // function to add and remove a shooting cooldown to and from the enemy 
    private IEnumerator Cooldown()
    {
        // adding cooldown
        cooldown = true;
        // waiting 1/4 of a second
        yield return new WaitForSeconds(0.75f);
        // removing the cooldown
        cooldown = false;
    }

    private void Shoot()
    {
        // if the enemy is not on a cooldown
        if (!cooldown)
        {
            // start the cooldown
            StartCoroutine(Cooldown());
            // setting _bullet to the bullet that has just been created
            GameObject _bullet = Instantiate(bullet);
            // setting the bullets position to the enemy
            _bullet.transform.position = transform.position;
            // adding 1 to the bullets in the scene
            bullets++;
            // renaming the bullet to bullet_ + the amount of bullets in the scene
            _bullet.name = "Bullet_" + bullets;
            // start the coroutine to cull the bullet
            StartCoroutine(CullBullets(_bullet));
        }
    }

    // coroutine that takes in a gameobject and culls it after a certain time
    private IEnumerator CullBullets(GameObject instance)
    {
        // wait 3 seconds
        yield return new WaitForSeconds(3);
        // destroy the gameobject that was passed in
        Destroy(instance);
        // remove 1 from the bullets integer
        bullets--;
    }

    // coroutine to move the enemy
    private IEnumerator MoveEnemy()
    {
        // endless loop
        while (true)
        {
            // wait 1 second
            yield return new WaitForSeconds(1);

            // if switch is false
            if (!_switch)
            {
                // if the x sorting order is equal to 5
                if (sortingorder_x == 5)
                {
                    // add 1 to the y sorting order
                    sortingorder_y++;
                    // set the x sorting order to 0
                    sortingorder_x = 0;
                    // set switch to true
                    _switch = true;
                    // move the enemy down
                    transform.Translate(0, -speed, 0);
                }
                else if (sortingorder_x < 5) // else if the x sorting order is less than 5 
                {
                    // move the enemy to the right by speed
                    transform.Translate(speed, 0, 0);
                    // increment the x sorting order
                    sortingorder_x++;
                }
            }

            // if switch is true
            if (_switch)
            {
                // if the x sorting order is equal to 5
                if (sortingorder_x == 5)
                {
                    // increment the y sorting order
                    sortingorder_y++;
                    // set the x sorting order to 0
                    sortingorder_x = 0;
                    // set switch to false which means we will run the top one again ^^^^
                    _switch = false;
                    // move the enemy down by speed
                    transform.Translate(0, -speed, 0);
                }
                else if (sortingorder_x < 5) // else if the x sorting order is less than 5 
                {
                    // move the enemy left by speed
                    transform.Translate(-speed, 0, 0);
                    // increment the x sorting order
                    sortingorder_x++;
                }
            }          
        }
    }

    public void IncreaseSpeed() // function to increase the speed of the enemy
    {
        speed += speed; // increase the speed by speed
    }
    
    private IEnumerator SwapSprite() // coroutine to swap the sprite every second
    {
        while (true) // infinite loop
        {
            yield return new WaitForSeconds(1f); // wait 1 second
            if (sprite_index == 0) // if the enemy is using the main sprite
            {
                main_sprite.SetActive(false); // disable the main sprite
                animated_sprite.SetActive(true); // enable the animated sprite
                sprite_index++; // increment sprite_index
            }
            else if (sprite_index == 1) // if the enemy is using the animated sprite
            {
                animated_sprite.SetActive(false); // disable the animated sprite
                main_sprite.SetActive(true); // enable the main sprite
                sprite_index--; // decrement the sprite_index
            }
        }
    }
}
