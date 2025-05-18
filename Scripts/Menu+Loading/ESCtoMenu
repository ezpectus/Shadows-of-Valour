using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



/*public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    private bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        // Курсор ВСЕГДА виден
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.SaveGame();
        LevelManager.Instance.LoadMainMenu();
    }
}*/

public class PauseMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitToMenuWithDelay());
        }
    }

    private IEnumerator ExitToMenuWithDelay()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.exitedToMenu = true;
            GameManager.Instance.SaveGame();
            Debug.Log("[PauseMenu] Save the game before exiting the menu...");
        }

        yield return new WaitForSeconds(3f); // Wait for Save to work for sure
        Time.timeScale = 1f;
        LevelManager.Instance.LoadMainMenu();
    }
}
