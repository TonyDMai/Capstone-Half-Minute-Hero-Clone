using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Over Handler
/// </summary>
public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryObject inventory; // Player Inventory 
    public InventoryObject equipment; // Player Equipment

    /// <summary>
    /// Enable the cursor when script loads
    /// </summary>
    private void Start()
    {
        Cursor.visible = true;
    }
    /// <summary>
    /// Quit the Game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Go to Title Screen
    /// </summary>
    public void TitleScreen() {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
