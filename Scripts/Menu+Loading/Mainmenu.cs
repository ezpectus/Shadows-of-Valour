using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    public Button loadGameButton;

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        if (loadGameButton != null)
            loadGameButton.interactable = File.Exists(path);
    }

    public void NewGame()
    {
        SaveSystem.DeleteSave();
        SceneManager.LoadScene("lvl1");
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        if (File.Exists(path))
        {
            GameManager.Instance.LoadGame();
        }
        else
        {
            Debug.Log("Сейв не найден. Загружаю первый уровень...");
            SceneManager.LoadScene("lvl1");
        }
    }


    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
