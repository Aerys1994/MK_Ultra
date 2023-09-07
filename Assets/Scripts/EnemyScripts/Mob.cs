using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    Mob mob;
    public string enemyName { get; protected set; }
    public int life { get; protected set; }
    public float speed { get; protected set; } 

    public virtual void Start() {
        mob = GetComponent<Mob>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            mob.life -= 1;

            if (mob.life <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
