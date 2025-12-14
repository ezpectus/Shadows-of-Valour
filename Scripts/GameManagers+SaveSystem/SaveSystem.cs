using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;



[System.Serializable]
public class PlayerData
{
    public int level;
    public int hp;
    public string currentScene;
}
public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");
    private static string KilledEnemiesPath => Path.Combine(Application.persistentDataPath, "killed_enemies.json");

    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Player save deleted.");
        }

        if (File.Exists(KilledEnemiesPath))
        {
            File.Delete(KilledEnemiesPath);
            Debug.Log("List of killed enemies removed.");
        }
    }
}

