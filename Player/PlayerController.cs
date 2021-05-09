using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player Controller Class. Controls movement in overworld
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d; //Players RigidBody Component
    private SpriteRenderer spriteRenderer; // player sprite display
    private Animator animator; // player animator
    public Player player; // player object
    public bool hasLeveledUp = false; // player level up condition

    public LayerMask dungeonGroundLayer; // The Dungeon Hitboxes


    public float speed; // How fast the player will move
    private Vector3 change; // The change in distance from original position

    /// <summary>
    /// A Method that will control actions that happens before the first frame of updating. Used to grab references to objects the player can interact with
    /// </summary>
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //obtain and assign the associated rigidbody
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// A method that will run on every frame, used to detect movement and other interactions
    /// </summary>
    void FixedUpdate()
    {
        change = Vector3.zero;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        if (moveHorizontal == -1){
            spriteRenderer.flipX = true;
        }
        if (moveHorizontal == 1) {
            spriteRenderer.flipX = false;
        }

        float moveVertical = Input.GetAxisRaw("Vertical"); 
        change = new Vector3(moveHorizontal, moveVertical);
        if (change != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            MoveCharacter();

        }
        else {
            animator.SetBool("isMoving", false);
        }
         
    }

    /// <summary>
    /// Function to move the character
    /// </summary>
    void MoveCharacter() {

        rb2d.MovePosition( transform.position + change * speed * Time.deltaTime);

        CheckForEncounters();

    }

    /// <summary>
    /// Check for random mob encounters. Save player data and will transition scenes when found
    /// </summary>
    private void CheckForEncounters() {

        if (Physics2D.OverlapCircle(transform.position, 0.2f, dungeonGroundLayer) != null) {
            if (Random.Range(1, 101) <= 1)
            {
                player.GetComponent<Player>().inBattle = true;
                player.GetComponent<Player>().SavePlayer();
                player.GetComponent<Player>().inventory.tempSave();
                player.GetComponent<Player>().equipment.tempSave();
                //Debug.Log("Found an Enemy");
                // Scene battleScene = SceneManager.GetSceneByName("Scenes/Battle");
                //  Debug.Log(battleScene.name);
                
                SceneManager.LoadScene("Scenes/Battle");
                // SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetSceneByName("Scenes/Battle"));
                //DontDestroyOnLoad(player);
                
            }
        }
    }

}
