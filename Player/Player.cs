using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
/// <summary>
/// The Player Object. HOuses all the stats and checks for collision in the overworld/village
/// </summary>
public class Player : MonoBehaviour
{

    public InventoryObject inventory; // Player inventory
    public InventoryObject equipment; //Player equipment

    private bool onBoss = false; //Condition if player is on boss Trigger
    private bool onVillage = false; //Condition if player is on village trigger
    private bool onVillageExit = false; //Condition if player is on exit village trigger

    public GameObject prompt; // Game Prompt

    public Animator animator; // Player Animator
    public bool isDead; //Condition if player is dead 

    //Player Stats
    public Attribute[] attributes;
    public int atk = 0;
    public int baseAtk = 3;
    public int baseDef = 2;
    public int def= 0;
    public int currentHealth = 20;
    public int maxHealth = 20;
    public int gold = 0;
    public int currentXP = 0;
    public int maxXP= 20;
    public int level = 1;

    // CHeck if player is in battle
    public bool inBattle = false;

    // Player Position
    public float playerPosX;
    public float playerPosY;

    /// <summary>
    /// Player Constructor. Loads the player data from the global game object
    /// </summary>
    public Player() {

        LoadPlayerData();
        if (atk == 0) { atk = baseAtk; }
        if (def == 0) { def = baseDef; }
        isDead = false;
    }
    /// <summary>
    /// Saves the player data temporarily to move between scenes.
    /// </summary>
    public void SavePlayer()
    {
        if (GameController.Instance == null)
        {
             GameController.Instance =  GameController.CreateNew();
        }

        GameController.Instance.currentHP = currentHealth;
        GameController.Instance.maxHP = maxHealth; 
        GameController.Instance.atk = atk;
        GameController.Instance.def = def;
        GameController.Instance.currentXP = currentXP;
        GameController.Instance.maxXP = maxXP;
        GameController.Instance.gold = gold;
        GameController.Instance.baseatk = baseAtk;
        GameController.Instance.baseDef = baseDef;
        GameController.Instance.Level = level;
        GameController.Instance.inBattle = inBattle;
        if (inBattle)
        {
            GameController.Instance.playerPosX = GetComponent<Transform>().position.x;
            GameController.Instance.playerPosY = GetComponent<Transform>().position.y;
        }
    }
    /// <summary>
    /// Load Player data to transition between scenes
    /// </summary>
    public void LoadPlayerData()
    {
        if (GameController.Instance.isNewGame)
        {


            GameController.Instance.Reset();
            Debug.Log("Resetting");
            inventory.Clear();
            equipment.Clear();
            inventory.tempSave();
            equipment.tempSave();

            GameController.Instance.isNewGame = false;
        }
        currentHealth = GameController.Instance.currentHP;
        maxHealth = GameController.Instance.maxHP;
        atk = GameController.Instance.atk;
        def = GameController.Instance.def;
        currentXP = GameController.Instance.currentXP;
        maxXP = GameController.Instance.maxXP;
        gold = GameController.Instance.gold;
        baseAtk = GameController.Instance.baseatk;
        baseDef = GameController.Instance.baseDef;
        level = GameController.Instance.Level;
        inBattle = GameController.Instance.inBattle;
        //this.transform = this.transform.position(new Vector3(GameController.Instance.playerPosX, GameController.Instance.playerPosY));
    }

    /// <summary>
    /// Check for collissions in battle. Controls if player takes damage
    /// </summary>
    /// <param name="collision">The Collision object in the entity</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var enemy = collision.collider.gameObject;
        // If collides with an enemy or player take damage.
        if (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss")) {
           // Debug.LogError(enemy);
            int dmg = (enemy.GetComponent<Mob>().atk - def);
            if (dmg > 0) { currentHealth -= dmg; }
            else { currentHealth--; }
            
        }
        
    }
    /// <summary>
    /// Collission detection for triggers
    /// </summary>
    /// <param name="collision">Collision object for triggers</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Check if boss trigger
        if (collision.collider.CompareTag("BossDoor"))
        {
            prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Press Enter to Fight Boss";
            prompt.SetActive(true);
            onBoss = true;
        }
        //Check if Village Trigger
        if (collision.collider.CompareTag("VillageDoor")) {
            prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Press Enter to go to Village";
            prompt.SetActive(true);
            onVillage = true;
        }
    }

    /// <summary>
    /// Check for collision in overworld / Village. Displays prompt if necessary
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();

        // Check if its an Item on the ground. Add to inventory and temporarily save it
        if (item)
        {
            Item _item = new Item(item.item);
            Debug.Log(_item.Name);
            if (inventory.AddItem(_item, 1)) {
                Debug.Log(_item.Id + " " + _item.Name);
                Destroy(collision.gameObject);
            }

            inventory.tempSave();
            inventory.TempLoad();
            equipment.TempLoad();
        }
        // Check if its the boss door
        if (collision.CompareTag("BossDoor"))
        {
            prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Press Enter to Fight Boss";
            prompt.SetActive(true);
            onBoss = true;
        }
        //Check if its the village trigger
        if (collision.CompareTag("VillageDoor"))
        {
            prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Press Enter to go to Village";
            prompt.SetActive(true);
            onVillage = true;
        }
        // Check if its the exit village trigger
        if (collision.CompareTag("VillageExit"))
        {
            prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Press Enter to leave Village";
            prompt.SetActive(true);
            onVillageExit = true;
        }
        //Otherwise disables
        if (collision.CompareTag("Untagged")) {
            prompt.SetActive(false);
            onBoss = false;
            onVillage = false;
            onVillageExit = false;
        }


    }
    /// <summary>
    /// Disables objects if leaving the trigger
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerExit2D(Collider2D collision)
    {
        prompt.SetActive(false);
        onBoss = false;
        onVillage = false;
        onVillageExit = false;
    }
    /// <summary>
    /// Method that fires when the object is first created a scene
    /// </summary>

    private void Start()
    {
        // New Game. Set new inventory
        if (GameController.Instance.isNewGame) {
            inventory.Clear();
            equipment.Clear();
            inventory.tempSave();
            equipment.tempSave();

            GameController.Instance.isNewGame = false;
        }
        //set attributes
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        //Set inventory slots
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

        animator = this.GetComponent<Animator>();
        LoadPlayerData();
        //Load Data
        if (GameController.Instance.isLoad)
        {
            inventory.Load();
            equipment.Load();

            inventory.tempSave();
            equipment.tempSave();
            inventory.TempLoad();
            equipment.TempLoad();

            GameController.Instance.isLoad = false;
        }
        else {
            inventory.tempSave();
            equipment.tempSave();
            inventory.TempLoad();
            equipment.TempLoad();
        }
        //Only save position if in overworld
        if (!inBattle)
        {
            this.transform.position = new Vector3(GameController.Instance.playerPosX, GameController.Instance.playerPosY);
            inventory.TempLoad();
            equipment.TempLoad();
        }

        inventory.TempLoad();
        equipment.TempLoad();
    }
    /// <summary>
    /// Get stats before updating equipment
    /// </summary>
    /// <param name="_slot"></param>
    public void OnBeforeSlotUpdate(InventorySlot _slot) {
        if (_slot.ItemObject == null) { return; }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }

    }
    /// <summary>
    /// 
    /// Update stats after putting an item in equipment slot
    /// </summary>
    /// <param name="_slot"></param>
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null) { return; }
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                        {
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Check for input every frame
    /// </summary>
    private void Update()
    {
        // Check for entering Boss
        if (Input.GetKeyDown(KeyCode.Return) && onBoss)
        {
            SavePlayer();
            SceneManager.LoadScene("Scenes/Boss");
        }
        //Check for entering village
        if (Input.GetKeyDown(KeyCode.Return) && onVillage)
        {
            SavePlayer();
            inventory.tempSave();
            equipment.tempSave();
            SceneManager.LoadScene("Scenes/Village");
        }
        //Check for leaving village
        if (Input.GetKeyDown(KeyCode.Return) && onVillageExit)
        {
            SavePlayer();
            inventory.tempSave();
            SceneManager.LoadScene("Scenes/Main");
        }
    }
    /// <summary>
    /// Set the attack and defense attributes
    /// </summary>
    /// <param name="attribute"></param>
    public void AttributeModified(Attribute attribute) {
        //Debug.Log(string.Concat(attribute.type, "was updated! Value is now ", attribute.value.ModifiedValue));
        if (attribute.type == Attributes.Attack) { atk = baseAtk + attribute.value.ModifiedValue; }
        if (attribute.type == Attributes.Defense) { def = baseDef + attribute.value.ModifiedValue; }
       // Debug.Log(string.Concat("Atk is now", atk));
       // Debug.Log(string.Concat("Def is now", def));
    }
    //Clear inventory when game is closed
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}
