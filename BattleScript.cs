using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// The main controller for the battle scenes in the game.
/// </summary>
public class BattleScript : MonoBehaviour
{
    public GameObject player; //Player Game object
    public Animator playerAnimator; //player animator object
    public GameObject enemy; //enemy object
    public Animator enemyAnimator; //enemy animator object
    public ItemDatabaseObject database; //Item Database Object
    public GameObject TextBox; //Textbox for victory message
    public Boolean msgShown = false; //Check if the message is shown
    public bool bossFight = false; //Check if player is fighting boss

    public InventoryObject inventory; //Player Inventory
    public bool speedUp = false; //Check for if player is clicking the speed up button
    public float speed; //Player velocity

    public bool gotHit;//Condition for if player got hit
    public bool isGrounded; //Condition if player is on the ground
    public float knockback; //Knockback value

    List<ItemObject> itemDrops = new List<ItemObject>();
    /// <summary>
    /// Function that runs when the scene is loaded
    /// </summary>
    public void Awake()
    {
        player = GameObject.Find("Player");
        Debug.Log(database.ItemObjects.Length);
        for (int i = 2; i < database.ItemObjects.Length; i++)
        {
            itemDrops.Add(database.ItemObjects[i]);
        }
        if (enemy.CompareTag("Boss"))
        {
            bossFight = true;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        gotHit = false;
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetBool("isMoving", true);
        enemyAnimator = enemy.GetComponent<Animator>();
        if (enemy.CompareTag("Boss"))
        {
            bossFight = true;
        }
    }

   /// <summary>
   /// A method that runs every frame. Handles inputs and makes players move and constantly check knockback
   /// </summary>
    void Update()
    {
        // Enable speed up of game
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedUp = true;
            speed = 6;
        }
        //Disable Speed Up
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedUp = false;
            speed = 3;
        }
        //Disables the victory message
        if (msgShown && Input.GetButton("Cancel"))
        {
            TextBox.SetActive(false);
            msgShown = false;

            inventory.Save();
            player.GetComponent<Player>().SavePlayer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        //Set Animation of player
        playerAnimator.SetBool("isMoving", true);

        CheckCollision(); // Check COllision
        //Only continue moving if Player is not getting knockbacked
        if (!gotHit)
        {
            if (player.GetComponent<Transform>().position.y <= -3.35)
            {
                //player.GetComponent<Animator>().SetBool("isHit", false);
                MoveEntity(player);
            }
            if (enemy.GetComponent<Transform>().position.y <= -3.4)
            {
                //enemy.GetComponent<Animator>().SetBool("isHit", false);
                MoveEntity(enemy);
            }
            if (enemy.CompareTag("Boss"))
            {
                if (enemy.GetComponent<Transform>().position.y <= -3.22)
                {
                    //enemy.GetComponent<Animator>().SetBool("isHit", false);
                    MoveEntity(enemy);
                }
            }
        }
        //Apply Knockback
        else if (gotHit)
        {
            ApplyKnockBack(player, true);
            ApplyKnockBack(enemy, false);
            gotHit = false;
        }
    }

    /// <summary>
    /// Move the Entitys
    /// </summary>
    /// <param name="entity"></param>
    public void MoveEntity(GameObject entity)
    {
        // Debug.Log(entity.name);
        Rigidbody2D rb2d = entity.GetComponent<Rigidbody2D>();
        //Move Player
        if (entity.name == "Player")
        {
            rb2d.velocity = new Vector2(1 * speed, rb2d.velocity.y);
        }
        //Move Enemy
        else
        {
            rb2d.velocity = new Vector2(-1 * speed, rb2d.velocity.y);
        }

    }
    /// <summary>
    /// Apply knockback to entities in collision 
    /// </summary>
    /// <param name="entity"> The current object being knocked back</param>
    /// <param name="hitFromRight">Direction being hit from</param>
    public void ApplyKnockBack(GameObject entity, bool hitFromRight)
    {
        Rigidbody2D rb2d = entity.GetComponent<Rigidbody2D>();
        //Player Knockback
        if (hitFromRight)
        {
            //player.GetComponent<Animator>().SetBool("isHit", true);
            rb2d.AddForce(new Vector2(-knockback, 10));
            //rb2d.velocity = new Vector2(-knockback , knockback);
        }
        //Enemy Knockback
        else
        {
            //enemy.GetComponent<Animator>().SetBool("isHit", true);
            rb2d.AddForce(new Vector2(knockback + 2, 10));
            // rb2d.velocity = new Vector2(knockback, knockback);
        }
    }
    /// <summary>
    /// Check if the player is in contact with the enemy
    /// </summary>
    public void CheckCollision()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D enemyCollider = enemy.GetComponent<BoxCollider2D>();
        if (playerCollider.IsTouching(enemyCollider))
        {
            gotHit = true;
        }

        CheckVictory();
    }

    /// <summary>
    /// Check if the Battle is over and deal with actions accordingly
    /// </summary>
    public void CheckVictory()
    {
        var playerScript = player.GetComponent<Player>();
        if (!bossFight)
        {
            //Check if player is alive and mob is dead
            if (!playerScript.isDead && enemy.GetComponent<Mob>().isDead)
            {

                int gold = UnityEngine.Random.Range(3, 10);
                int xp = UnityEngine.Random.Range(4, 8);
                playerScript.currentXP += xp;
                playerScript.gold += gold;

                bool gotLevel = CheckLevelUp();

                ////TODO: Add drops to inventory
                //int rng = UnityEngine.Random.Range(0, 101);
                //if (rng < 100) {
                //    Item item = (itemDrops[UnityEngine.Random.Range(0, itemDrops.Count)].data);

                TextBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Concat(" You have defeated the Lizard! \n Gained : ", gold, "g and ", xp, " EXP");
                if (gotLevel) {
                    TextBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Concat(" You Leveled Up! ");
                }
                TextBox.GetComponentInChildren<TextMeshProUGUI>().text += string.Concat("\n PRESS ESCAPE TO DISMISS");
                //    if (inventory.AddItem(item, 1))
                //    {
                //        TextBox.GetComponentInChildren<TextMeshProUGUI>().text += string.Concat("\n ", item.Name, " was added to your inventory!\n Press Escape to dismiss");
                //    }
                //    else {
                //        TextBox.GetComponentInChildren<TextMeshProUGUI>().text += string.Concat("\n Inventory is full,", item.Name," was disgarded \n Press Escape to dismiss");
                //    }
                TextBox.SetActive(true);
                msgShown = true;
                //}

                Destroy(enemy);

                playerScript.inBattle = false;
                //Double check to make sure player is not dead
                if (playerScript.currentHealth <= 0)
                {
                    SceneManager.LoadScene("Scenes/GameOver");
                }
            }
            // If player is dead, transition game scenes
            else if (playerScript.currentHealth <= 0)
            {
                SceneManager.LoadScene("Scenes/GameOver");
            }
        }
        // If Just the mob is dead
        else
        {
            if (enemy.GetComponent<Mob>().currentHealth <= 0)
            {
                SceneManager.LoadScene("Scenes/Victory");
            }
            else if (playerScript.currentHealth <= 0)
            {
                SceneManager.LoadScene("Scenes/GameOver");
            }
        }
    }

    /// <summary>
    /// Checks if player should level up and runs the corresponding level up function
    /// </summary>
    public bool CheckLevelUp()
    {
        var playerScript = player.GetComponent<Player>();
        if (playerScript.currentXP >= playerScript.maxXP)
        {
            LevelUp();
            return true;
        }
        return false;
    }
    /// <summary>
    /// Level Up function
    /// Incrememnts player level by 1, resets the current hp to max and increases stats
    /// </summary>
    public void LevelUp()
    {
        var playerScript = player.GetComponent<Player>();
        playerScript.level++;
        playerScript.maxXP = (int)(playerScript.maxXP * 2);
        playerScript.maxHealth += 5;
        playerScript.currentHealth = playerScript.maxHealth;
        playerScript.baseAtk++;
        playerScript.baseDef++;

    }



}
