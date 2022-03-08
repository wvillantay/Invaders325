using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // every frame
    private void Update()
    {
        // moving the bullet up 0.01 units every frame
        transform.Translate(0, -0.002f, 0);
    }
}
