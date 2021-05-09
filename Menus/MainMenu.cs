using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu handler class
/// </summary>
public class MainMenu : MonoBehaviour
{
    public InventoryObject inventory; //player inventory
    public InventoryObject equipment; //player equipment

    /// <summary>
    /// Disaplays Cursor on Load
    /// </summary>
    private void Start()
    {
        Cursor.visible = true;
    }
    /// <summary>
    /// Start a new Game
    /// </summary>
    public void NewGame() {
        GameController.Instance.isNewGame = true;
        GameController.Instance.tempSave();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    /// <summary>
    /// Load Previous Game
    /// </summary>
    public void LoadGame() {
        GameController.Instance.Load();
        GameController.Instance.isLoad = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Quit Game
    /// </summary>
    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
