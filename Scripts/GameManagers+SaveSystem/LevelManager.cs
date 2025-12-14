using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private int currentLevel = 1;
    private int maxLevels = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void LoadNextLevel()
    {
        if (currentLevel < maxLevels)
        {
            currentLevel++;
            LoadLevel(currentLevel);
        }
        else
        {
            LoadEndScene();
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("loading_sc");
        StartCoroutine(LoadSceneAfterDelay("lvl" + level));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("catscene_end");
    }
}
