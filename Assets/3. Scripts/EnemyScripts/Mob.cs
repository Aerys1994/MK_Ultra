using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    Mob mob;
    public string enemyName { get; protected set; }

    [SerializeField]
    private int life;
    public float speed { get; protected set; }

    private bool damageApplied = false;
    private float damageCooldown = 0.3f;
    protected SpriteRenderer sprite;
    protected Blink material;


    public virtual void Start() {
        mob = GetComponent<Mob>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!damageApplied && collision.CompareTag("Weapon"))
        {
            damageApplied = true;
            StartCoroutine(ResetDamageCooldown());
            mob.life -= 1;

            if (mob.life <= 0) {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ResetDamageCooldown()
    {
        // Resets the damageApplied in order for the mob to be damageable again
        // This prefents multiple collisions being triggered, thus the player
        // makes more damage than it should. Also prevents for the player just
        // spamming attacks and gancking the mob
        sprite.material = material.blink;
        yield return new WaitForSeconds(damageCooldown);
        damageApplied = false;
        sprite.material = material.original;

    }
}
