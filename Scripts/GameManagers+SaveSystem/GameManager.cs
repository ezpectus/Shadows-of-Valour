using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;


[System.Serializable]
public class SaveData
{
    public int hp;
    public Vector3 position;
    public string scene;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerHealth player;
    public int enemyCount = 0;
    public string currentScene;

    private string savePath;
    private bool isLoadingFromSave = false;
    private bool allowLevelTransition = true;

    public bool exitedToMenu = false; // Added: if you enter the menu manually, it does not go to the level
    private HashSet<string> killedEnemies = new();
    private string KilledEnemiesPath => Path.Combine(Application.persistentDataPath, "killed_enemies.json");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        currentScene = SceneManager.GetActiveScene().name;
        RefreshSceneData();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;
        StartCoroutine(DelayedInit());
    }

    private IEnumerator DelayedInit()
    {
        yield return new WaitForSeconds(0.4f);
        RefreshSceneData();
        isLoadingFromSave = false;
        allowLevelTransition = true;
        exitedToMenu = false; // Reset the flag when the scene is loaded
    }

    private void RefreshSceneData()
    {
        player = FindObjectOfType<PlayerHealth>();
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("[GameManager] Enemies found: " + enemyCount);
    }

    public void EnemySpawned() => enemyCount++;

    public void EnemyDefeated()
    {
        enemyCount--;
        if (enemyCount <= 0 && allowLevelTransition && !exitedToMenu) // a new flag is taken into account
        {
            StartCoroutine(WaitBeforeNextLevel());
        }
    }

    private IEnumerator WaitBeforeNextLevel()
    {
        Debug.Log("[GameManager] Victory! Transition in 2 seconds.");
        allowLevelTransition = false;
        yield return new WaitForSeconds(2f);
        LevelManager.Instance.LoadNextLevel();
    }

    public void SaveGame()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "menu" || scene == "loading_sc" || scene == "catscene_end")
        {
            Debug.Log($"[GameManager] Scene {scene} - autosave skipped.");
            return;
        }

        SaveData data = new()
        {
            hp = player ? player.CurrentHealth : 100,
            position = player ? player.transform.position : Vector3.zero,
            scene = currentScene
        };

        File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        Debug.Log("[GameManager] Game saved successfully.");
    }

    public void MarkEnemyAsKilled(string id)
    {
        if (!killedEnemies.Contains(id))
        {
            killedEnemies.Add(id);
            SaveKilledEnemies();
        }
    }

    public bool IsEnemyAlreadyKilled(string id)
    {
        return killedEnemies.Contains(id);
    }

    private void SaveKilledEnemies()
    {
        File.WriteAllText(KilledEnemiesPath, JsonUtility.ToJson(new KilledEnemiesWrapper { ids = killedEnemies.ToList() }, true));
    }

    private void LoadKilledEnemies()
    {
        if (File.Exists(KilledEnemiesPath))
        {
            var json = File.ReadAllText(KilledEnemiesPath);
            var wrapper = JsonUtility.FromJson<KilledEnemiesWrapper>(json);
            killedEnemies = new HashSet<string>(wrapper.ids);
        }
    }

    [Serializable]
    private class KilledEnemiesWrapper
    {
        public List<string> ids;
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("Save  not found!");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        isLoadingFromSave = true;
        allowLevelTransition = false;
        exitedToMenu = false;

        SceneManager.LoadScene(data.scene);
        StartCoroutine(ApplyLoadedData(data));
    }

    private IEnumerator ApplyLoadedData(SaveData data)
    {
        yield return new WaitForSeconds(0.5f);

        player = FindObjectOfType<PlayerHealth>();
        if (player != null)
        {
            player.SetHP(data.hp);
            player.transform.position = data.position;
        }
        else Debug.LogWarning("[GameManager] Player not found!");

        yield return new WaitForSeconds(0.5f);
        RefreshSceneData();

        yield return new WaitForSeconds(0.3f);
        isLoadingFromSave = false;
        allowLevelTransition = true;
        exitedToMenu = false;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(SaveGame), 10f, 10f);
    }
}
