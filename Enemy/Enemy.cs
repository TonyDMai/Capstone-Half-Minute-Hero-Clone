using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth;
    public Animator animator;
    public int maxHealth;
    public int def;
    public int atk;
    public bool isDead = false;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    public Enemy()
    {
        currentHealth = 10;
        maxHealth = 10;
        atk = 3;
        def = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        var enemy = collision.collider.gameObject;
        if (enemy.CompareTag("Player"))
        {
            // Debug.LogError(enemy);
            int dmg = (enemy.GetComponent<Player>().atk - def);
            if (dmg > 0) { currentHealth -= dmg; }
            else { currentHealth--; }
        }
        if (currentHealth <= 0)
        {
            isDead = true;
            //Destroy(this);
        }
    }
}
