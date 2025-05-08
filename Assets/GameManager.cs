using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ProjectileThrow playerLauncher;

    [SerializeField] int beachBallsLeft = 10;
    [SerializeField] int ducksLeft = 9;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject youWinScreen;
    [SerializeField] private GameObject pauseScreen;

    private bool isPaused = false;
    private bool gameStarted = false;
    public static bool skipStartScreen = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameStarted = true;
        Time.timeScale = 1f;

        if (HUD.instance != null)
        {
            HUD.instance.setDucksLeft(ducksLeft);
            HUD.instance.setWaterLeft(beachBallsLeft);
        }
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            TogglePause();
        }
    }

    public void StartGame()
    {
        skipStartScreen = true;
        SceneManager.LoadScene("Game");
    }

    public void RestartGame()
    {
        skipStartScreen = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void UseShot(int value = 1)
    {
        beachBallsLeft -= value;
        HUD.instance.setWaterLeft(beachBallsLeft);

        if (beachBallsLeft <= 0 && ducksLeft > 0)
        {
            gameOverScreen.SetActive(true);
            
            Time.timeScale = 0f;
        }
    }

    public void AddShot(int value)
    {
        beachBallsLeft += value;
        HUD.instance.setWaterLeft(beachBallsLeft);
        playerLauncher.hasLaunched = false;
    }

    public void DuckHit()
    {
        ducksLeft--;
        HUD.instance.setDucksLeft(ducksLeft);

        if (ducksLeft <= 0 && beachBallsLeft >= 0)
        {
            youWinScreen.SetActive(true);
            
            Time.timeScale = 0f;
        }
        else
        {
            playerLauncher.hasLaunched = false;
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Unpause the game just in case
        SceneManager.LoadScene("Menu"); // Replace "Menu" with your actual scene name if different
    }

    public bool canShoot()
    {
        return beachBallsLeft > 0;
    }
}
