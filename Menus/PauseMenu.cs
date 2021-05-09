using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Main Class responsible for the functionality of the pause menu in game
/// </summary>
public class PauseMenu : MonoBehaviour

{
    public bool isPaused = false; // Menu
    public GameObject menu; // Pause Menu
    public GameObject thePlayer; // Player Object
    public InventoryObject inventory; // Player Inventory
    public InventoryObject equipment; // Player Equipment
    public GameObject IandEPanel; //The Equip and Inventory Panel
    public GameObject inventoryPanel; // Inventory Panel
    public GameObject statsPanel; // Stats Panel
    public GameObject descriptionPanel; // Description Panel
    public GameObject equipPanel; // Equipment Panel
    public GameObject equipButton; // Equipment button
    public GameObject Stats; // Stats label
    public GameObject gold; // Gold Label

    /// <summary>
    /// Show Inventory Panel by setting corresponding objects to true
    /// </summary>
    public void showInventory()
    {

        IandEPanel.SetActive(true);

        inventory.TempLoad();
        equipment.TempLoad();

        inventoryPanel.SetActive(true);
        equipPanel.SetActive(true);
        statsPanel.SetActive(false);
        descriptionPanel.SetActive(false);
        equipButton.SetActive(false);


        inventory.TempLoad();
        equipment.TempLoad();
        gold.GetComponent<TextMeshProUGUI>().text = thePlayer.GetComponent<Player>().gold.ToString() + "g";
    }

    /// <summary>
    /// Saves the game
    /// </summary>
    public void Save() {
        Debug.Log("Save");
        inventory.Save();
        equipment.Save();
        GameController.Instance.Save();
    }

    /// <summary>
    /// Show Stats Panel
    /// </summary>
    public void showStats()
    {
        Player player = thePlayer.GetComponent<Player>();
        // string playerStats = thePlayer.GetComponent<PlayerController>
        IandEPanel.SetActive(false);
        equipPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        statsPanel.SetActive(true);
        descriptionPanel.SetActive(false);
        equipButton.SetActive(false);

        Stats.GetComponent<TextMeshProUGUI>().text = player.level +"\n" + player.currentHealth + " / " + player.maxHealth + "\n" + player.atk + "\n" + player.def + "\n" + player.currentXP + "\n" + player.maxXP + "\n" + player.gold;
    }

    /// <summary>
    /// Show Description Panel
    /// </summary>
    public void showDescription()
    {
        descriptionPanel.SetActive(true);
        equipButton.SetActive(false);
    }

    /// <summary>
    /// Quit Game
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    /// <summary>
    /// Return to the game and disable menus
    /// </summary>
    public void ReturnToGame() {
        IandEPanel.SetActive(false);
        equipPanel.SetActive(false);
        equipButton.SetActive(false);
        inventoryPanel.SetActive(false);
        statsPanel.SetActive(false);
        descriptionPanel.SetActive(false);
        thePlayer.GetComponent<PlayerController>().enabled = true;
        menu.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Go back to title screen
    /// </summary>
    public void ReturnToTitle() {
        menu.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            //Disable User Controls
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                Cursor.visible = true;
                menu.SetActive(true);
                Debug.Log(SceneManager.GetActiveScene().name);
                if (SceneManager.GetActiveScene().name != "Village")
                {
                    thePlayer.GetComponent<PlayerController>().enabled = false;
                }
            }
            //Re-enable User Controls
            else
            {
                IandEPanel.SetActive(false);
                equipPanel.SetActive(false);
                inventoryPanel.SetActive(false);
                statsPanel.SetActive(false);
                descriptionPanel.SetActive(false);
                Debug.Log(SceneManager.GetActiveScene().name);
                if (SceneManager.GetActiveScene().name != "Village")
                {
                    thePlayer.GetComponent<PlayerController>().enabled = true;
                }
                menu.SetActive(false);
                Cursor.visible = false;
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }
}
