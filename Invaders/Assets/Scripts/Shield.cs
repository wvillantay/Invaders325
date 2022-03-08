using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // declaring a private int for the health 
    private int health;

    // when the script starts
    private void Start()
    {
        // set the health to 40
        health = 40;
    }

    // when something collides with the shield
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if what hit the shield was an enemies bullet
        if (collision.gameObject.tag == "EnemyBullet")
        {
            // set the bullet to inactive
            collision.gameObject.SetActive(false);
            // do 10 damage to the shield
            Damage(10);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            // set the bullet to inactive
            collision.gameObject.SetActive(false);
        }
    }

    // function that takes in an int and sets health equal to itself minus the int
    public void Damage(int amount)
    {
        // health equals health minus amount
        health -= amount;
        // if health is less than or equal to zero
        if (health <= 0)
        {
            // set health to 0
            health = 0;
            // destroy the shield
            Destroy(transform.gameObject);
        }
    }
}
