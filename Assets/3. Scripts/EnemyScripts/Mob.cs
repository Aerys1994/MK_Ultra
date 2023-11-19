using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    Mob mob;

    // Standard misc variables
    public string enemyName { get; protected set; }
    [SerializeField]
    private int life;
    public float speed { get; protected set; }

    // Damage management and misc combat variables
    protected Rigidbody2D rb;
    private bool damageApplied = false;
    private float damageCooldown = 0.3f;
    public float knockbackForceX;
    public float knockbackForceY;
    public int damageToGive;

    // Visual effects variables
    protected SpriteRenderer sprite;
    protected Blink material;


    public virtual void Start() {
        mob = GetComponent<Mob>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!damageApplied && collision.CompareTag("Weapon"))
        {
            damageApplied = true;
            StartCoroutine(ResetDamageCooldown());

            if (collision.transform.position.x < mob.transform.position.x)
            {
                rb.AddForce(new Vector2(mob.knockbackForceX, mob.knockbackForceY), ForceMode2D.Force);
            } else
            {
                rb.AddForce(new Vector2(-mob.knockbackForceX, mob.knockbackForceY), ForceMode2D.Force);
            }

            
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
