using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreenManager : MonoBehaviour
{
    public string mainMenuScene = "menu"; // Укажи название сцены с меню
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("Любая клавиша нажата! Попытка загрузки сцены: " + mainMenuScene);
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        Debug.Log("Попытка загрузки сцены: " + mainMenuScene);
        SceneManager.LoadScene(mainMenuScene);
    }
}

