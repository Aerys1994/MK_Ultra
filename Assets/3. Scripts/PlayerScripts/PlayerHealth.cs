using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerHealth : MonoBehaviour
{

   

    public int health;
    public int maxHealth;
    private float damageCooldown = 0.3f;
    private bool damageApplied = false;
    protected SpriteRenderer sprite;
    protected Blink material;
    public float knockbackForceX;
    public float knockbackForceY;
    protected Rigidbody2D rb;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        health = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();

    }

    private void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    // See code on Mob for more info on this script
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!damageApplied && collision.CompareTag("Enemy"))
        {
            health -= collision.GetComponent<Mob>().damageToGive;



            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            StartCoroutine(ResetDamageCooldown());

            

            damageApplied = true;
        }
    }

    private IEnumerator ResetDamageCooldown()
    {
        sprite.material = material.blink;
        yield return new WaitForSeconds(damageCooldown);
        damageApplied = false;
        sprite.material = material.original;
    }

}
