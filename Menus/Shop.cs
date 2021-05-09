using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Main Shop Handler
/// </summary>
public class Shop : MonoBehaviour
{
    //Player Data
    public Player player;
    private Player playerScript;
    public InventoryObject inventory;
    public GameObject inventoryPanel;

    public ItemDatabaseObject database; //item database
    public GameObject dialog; // textfield
    public GameObject gold; // gold textfield
    /// <summary>
    /// Get player script
    /// </summary>
    public void Start()
    {
        playerScript = player.GetComponent<Player>();
    }
    /// <summary>
    /// Buy Iron Sword
    /// </summary>
    public void BuyIronSword() 
    {
        if (playerScript.gold >= 15)
        {
            inventory.AddItem(database.ItemObjects[3].data, 1);

            inventory.tempSave();
            playerScript.gold -= 15;
            gold.GetComponent<TextMeshProUGUI>().text = player.gold.ToString();

            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You bought a " + database.ItemObjects[3].data.Name;
            dialog.SetActive(true);
            inventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        }
        else {
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You do not have enough gold to buy a " + database.ItemObjects[3].data.Name;
            dialog.SetActive(true);
        }
    }

    /// <summary>
    /// Buy Iron Shield
    /// </summary>
    public void BuyIronShield()
    {
        if (playerScript.gold >= 10)
        {
            inventory.AddItem(database.ItemObjects[2].data, 1);
            inventory.tempSave();
            playerScript.gold -= 10;
            gold.GetComponent<TextMeshProUGUI>().text = player.gold.ToString();

            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You bought a " + database.ItemObjects[2].data.Name;
            dialog.SetActive(true);
            inventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        }
        else
        {
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You do not have enough gold to buy a " + database.ItemObjects[2].data.Name;
            dialog.SetActive(true);
        }
    }

    /// <summary>
    /// Buy Iron Helm
    /// </summary>
    public void BuyIronHelm()
    {
        if (playerScript.gold >= 10)
        {
            inventory.AddItem(database.ItemObjects[1].data, 1);
            inventory.tempSave();
            playerScript.gold -= 10;
            gold.GetComponent<TextMeshProUGUI>().text = player.gold.ToString();

            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You bought a " + database.ItemObjects[1].data.Name;
            dialog.SetActive(true);
            inventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        }
        else
        {
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You do not have enough gold to buy a " + database.ItemObjects[1].data.Name;
            dialog.SetActive(true);
        }
    }
    /// <summary>
    /// Buy paladin Shield
    /// </summary>
    public void BuyPaladinShield()
    {
        if (playerScript.gold >= 25)
        {
            inventory.AddItem(database.ItemObjects[6].data, 1);
            inventory.tempSave();
            playerScript.gold -= 25;
            gold.GetComponent<TextMeshProUGUI>().text = player.gold.ToString();

            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You bought a " + database.ItemObjects[6].data.Name;
            dialog.SetActive(true);
            inventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        }
        else
        {
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You do not have enough gold to buy a " + database.ItemObjects[6].data.Name;
            dialog.SetActive(true);
        }
    }
    /// <summary>
    /// Buy Paladin Helmet
    /// </summary>
    public void BuyPaladinHelm()
    {
        if (playerScript.gold >= 25)
        {
            inventory.AddItem(database.ItemObjects[5].data, 1);
            inventory.tempSave();
            playerScript.gold -= 25;
            gold.GetComponent<TextMeshProUGUI>().text = player.gold.ToString();

            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You bought a " + database.ItemObjects[5].data.Name;
            dialog.SetActive(true);
            inventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        }
        else
        {
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You do not have enough gold to buy a " + database.ItemObjects[5].data.Name;
            dialog.SetActive(true);
        }
    }
    /// <summary>
    /// Buy Excalibur
    /// </summary>
    public void BuyExcalibur()
    {
        if (playerScript.gold >= 50)
        {
            inventory.AddItem(database.ItemObjects[0].data, 1);
            inventory.tempSave();
            playerScript.gold -= 50;
            gold.GetComponent<TextMeshProUGUI>().text = player.gold.ToString();

            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You bought a " + database.ItemObjects[0].data.Name;
            dialog.SetActive(true);
            inventoryPanel.GetComponent<InventoryManager>().UpdateInventory();
        }
        else
        {
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You do not have enough gold to buy a " + database.ItemObjects[0].data.Name;
            dialog.SetActive(true);
        }
    }

}
