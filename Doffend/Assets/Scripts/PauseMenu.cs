using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject HealthUI;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        playerInput.Player.Pause.performed += ctx => MenuHandle();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HealthUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        HealthUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void MenuHandle()
    {
        if(GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
