using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MobType { 
Lizard,
Boss
}
public class Mob : MonoBehaviour
{
    public int currentHealth;
    public MobType type;
    public Animator animator;
    public int maxHealth;
    public int def;
    public int atk;
    public bool isDead = false;

    private void Start()
    {
        if (this.CompareTag("Enemy")) { 
            type = MobType.Lizard;
            maxHealth = 10;
            currentHealth = 10;
            def = 3;
            atk = 2;
        }
        else if (this.CompareTag("Boss")) {
            currentHealth = 30;
            maxHealth = 30;
            atk = 6;
            def = 4;
            type = MobType.Boss; 
        }
        animator = this.GetComponent<Animator>();
    }
    public Mob() {
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
        if (currentHealth <= 0) { isDead = true; }
    }
}
