using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/// <summary>
/// A global object used to transfer player stats between game scenes
/// </summary>
[Serializable]
public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool isNewGame = false;
    public bool isLoad;
    public string savePath;
    public string tempSavePath;

    //Game Stats
    public int currentHP;
    public int maxHP;
    public int currentXP;
    public int maxXP;
    public int atk;
    public int def;
    public int gold;
    public int baseatk;
    public int baseDef;
    public int Level;
    public bool inBattle;
    public float playerPosX;
    public float playerPosY;
    /// <summary>
    /// Set the gameobjectg and save default stats
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Instance.currentHP = 20;
            Instance.maxHP = 20;
            Instance.currentXP = 0;
            Instance.maxXP = 20;
            Instance.atk = 3;
            Instance.def = 2;
            Instance.gold = 0;
            Instance.baseatk = 3;
            Instance.baseDef = 2;
            Instance.Level = 1;
        }
        //Can only be one instance of this
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Save Player data
    /// </summary>
    [ContextMenu("Save")]
    public void Save()
    {
        int[] playerData = new int[] { Instance.currentHP, Instance.maxHP, Instance.currentXP, Instance.maxXP, Instance.atk, Instance.def, Instance.gold, Instance.baseatk, Instance.baseDef, Instance.Level };
        Debug.Log("Save Data" + playerData[0]);

        ///Harder to edit save
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }
    /// <summary>
    /// Temp save player data
    /// </summary>
    public void tempSave() {
        int[] playerData = new int[] { Instance.currentHP, Instance.maxHP, Instance.currentXP, Instance.maxXP, Instance.atk, Instance.def, Instance.gold, Instance.baseatk, Instance.baseDef, Instance.Level };
        Debug.Log("Save Data" + playerData.ToString());

        ///Harder to edit save
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, tempSavePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    /// <summary>
    /// Method to load the inventory from a file
    /// </summary>
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            ///Load Easy to edit Save
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            ///Load Hard to edit save
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            int[] playerData = (int[])formatter.Deserialize(stream);

            Debug.Log(playerData.ToString());



            stream.Close();
        }
    }
    /// <summary>
    /// Temp Load Inventory
    /// </summary>
    public void tempLoad() {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, tempSavePath), FileMode.Open, FileAccess.Read);
        int[] playerData = (int[])formatter.Deserialize(stream);

        Debug.Log(playerData.ToString());

        Instance = this;
        Instance.currentHP = playerData[0];
        Instance.maxHP = playerData[1];
        Instance.currentXP = playerData[2];
        Instance.maxXP = playerData[3];
        Instance.atk = playerData[4];
        Instance.def = playerData[5];
        Instance.gold = playerData[6];
        Instance.baseatk = playerData[7];
        Instance.baseDef = playerData[8];
        Instance.Level = playerData[9];


        stream.Close();
    }
    /// <summary>
    /// Make a new controller
    /// </summary>
    /// <returns></returns>
    public static GameController CreateNew() {
        Instance =  new GameController();
        return Instance;
    }
    /// <summary>
    /// Resets the data
    /// </summary>
    public void Reset()
    {
        Instance.currentHP = 20;
        Instance.maxHP = 20;
        Instance.currentXP = 0;
        Instance.maxXP = 20;
        Instance.atk = 3;
        Instance.def = 2;
        Instance.gold = 0;
        Instance.baseatk = 3;
        Instance.baseDef = 2;
        Instance.Level = 1;
    }
}
