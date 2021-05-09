using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Inventory Manager Class for the Shop Menu 
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryObject inventory; // Player Inventory
    public Player player; // Player Object
    public GameObject gold; //Gold Label

    //Item Quantities
    public GameObject wSwordQuantity;
    public GameObject iSwordQuantity;
    public GameObject excaliburQuantity;
    public GameObject wShieldQuantity;
    public GameObject iShieldQuantity;
    public GameObject pShieldQuantity;
    public GameObject lHelmQuantity;
    public GameObject iHelmQuantity;
    public GameObject pHelmQuantity;

    //TextBox for Game Messages
    public GameObject dialog;


    Dictionary<string, int> inInventory = new Dictionary<string, int>(); // Dictionary of items in the inventory

    /// <summary>
    /// Get all items in the inventory and add it to the dictionary
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> GetInventory()
    {
        Dictionary<string, int> itemsInInventory = new Dictionary<string, int>();
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item != null)
            {
                if (!itemsInInventory.ContainsKey(inventory.container.Slots[i].item.Name))
                {
                    itemsInInventory.Add(inventory.container.Slots[i].item.Name, 1);
                }
                else
                {
                    itemsInInventory[inventory.container.Slots[i].item.Name]++;
                }
            }
        }
        return itemsInInventory;
    }

    /// <summary>
    /// Sell Iron Sword
    /// </summary>
    public void SellIronSword() {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Iron Sword")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 7;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Iron Sword for 7g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Iron Sword in your inventory to sell";
            dialog.SetActive(true);
            
        }
    }
    /// <summary>
    /// Sell Iron Helm
    /// </summary>
    public void SellIronHelm()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Iron Helmet")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 5;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Iron Helmet for 5g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Iron Helmet in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Iron Shield
    /// </summary>
    public void SellIronShield()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Iron Shield")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 5;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Iron Shield for 5g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Iron Shield in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Wooden Sword
    /// </summary>
    public void SellWoodenSword()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Wooden Sword")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 2;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Wooden Sword for 2g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Wooden Sword in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Leather Hat
    /// </summary>
    public void SellLeatherHat()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Leather Hat")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 1;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Leather Hat for 1g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Leather Hat in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Wooden Shield
    /// </summary>
    public void SellWoodenShield()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Wooden Shield")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 1;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Wooden Shield for 1g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Wooden Shield in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Paladin Shield
    /// </summary>
    public void SellPaladinShield()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Paladin Shield")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 15;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Paladin Shield for 15g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Paladin Shield in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Paladin Helm
    /// </summary>
    public void SellPaladinHelm()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Paladin Helmet")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 15;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Paladin Helmet for 15g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Paladin Helmet in your inventory to sell";
            dialog.SetActive(true);

        }
    }
    /// <summary>
    /// Sell Excalibur
    /// </summary>
    public void SellExcalibur()
    {
        for (int i = 0; i < inventory.container.Slots.Length; i++)
        {
            if (inventory.container.Slots[i].item.Name == "Excalibur")
            {
                inventory.container.Slots[i].RemoveItem();
                player.GetComponent<Player>().gold += 25;
                gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
                UpdateInventory();
                dialog.GetComponentInChildren<TextMeshProUGUI>().text = "Sold an Excalibur for 25g";
                dialog.SetActive(true);
                return;
            }
            dialog.GetComponentInChildren<TextMeshProUGUI>().text = "You did not have an Excalibur in your inventory to sell";
            dialog.SetActive(true);

        }
    }

    /// <summary>
    ///     Update Inventory on load
    /// </summary>

    private void Start()
    {

        UpdateInventory();

    }

    /// <summary>
    /// Updates the inventory to display the correct quantity count and gold count
    /// </summary>
    public void UpdateInventory()
    {
        inInventory = GetInventory();
        if (inInventory.ContainsKey("Iron Sword")) { iSwordQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Iron Sword"].ToString(); }
        else { iSwordQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Wooden Sword")) { wSwordQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Wooden Sword"].ToString(); }
        else { wSwordQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Excalibur")) { excaliburQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Excalibur"].ToString(); }
        else { excaliburQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Leather Hat")) { lHelmQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Leather Hat"].ToString(); }
        else { lHelmQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Iron Helmet")) { iHelmQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Iron Helmet"].ToString(); }
        else { iHelmQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Paladin Helmet")) { pHelmQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Paladin Helmet"].ToString(); }
        else { pHelmQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Wooden Shield")) { wShieldQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Wooden Shield"].ToString(); }
        else { wShieldQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Iron Shield")) { iShieldQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Iron Shield"].ToString(); }
        else { iShieldQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        if (inInventory.ContainsKey("Paladin Shield")) { pShieldQuantity.GetComponent<TextMeshProUGUI>().text = inInventory["Paladin Shield"].ToString(); }
        else { pShieldQuantity.GetComponent<TextMeshProUGUI>().text = "0"; }

        gold.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().gold.ToString();
    }
}
