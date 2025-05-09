using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    
    public ProjectileThrow playerLauncher;

    // Variables to track the number of beach balls and ducks remaining
    [SerializeField] int beachBallsLeft = 10;
    [SerializeField] int ducksLeft = 9;

    // UI elements to show the different screens (game over, win, and pause)
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject youWinScreen;
    [SerializeField] private GameObject pauseScreen;

    // Music control
    [SerializeField] private AudioSource backgroundMusic;

    // manage pause state and track whether the game has started
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

        //  update the UI with the current number of ducks and beach balls
        if (HUD.instance != null)
        {
            HUD.instance.setDucksLeft(ducksLeft);
            HUD.instance.setWaterLeft(beachBallsLeft);
        }
    }

    
    private void Update()
    {
        // check if the Escape key is presse
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            TogglePause(); // Toggle the pause state if Escape is pressed
        }
    }

    // start a new game by loading the Game scene
    public void StartGame()
    {
        skipStartScreen = true; // Skip the start screen if the game is started
        SceneManager.LoadScene("Game"); // Load the Game scene
    }

    // restart the game by reloading the current scene
    public void RestartGame()
    {
        skipStartScreen = true; // Skip the start screen if the game is restarted
        Time.timeScale = 1f; // Ensure the game is running at normal speed
        SceneManager.LoadScene("Game"); // Reload the Game scene
    }

   
    public void UseShot(int value = 1)
    {
        beachBallsLeft -= value; 
        HUD.instance.setWaterLeft(beachBallsLeft); 

       
        if (beachBallsLeft <= 0 && ducksLeft > 0)
        {
            // stop the music and show the game over screen
            if (backgroundMusic != null) backgroundMusic.Stop();
            gameOverScreen.SetActive(true); // Display the game over screen
            Time.timeScale = 0f; // Pause the game
        }
    }

    // add extra beach balls and update the HUD
    public void AddShot(int value)
    {
        beachBallsLeft += value; // Add the specified number of beach balls
        HUD.instance.setWaterLeft(beachBallsLeft); // Update the HUD with the new number of beach balls
        playerLauncher.hasLaunched = false; // Reset the launcher
    }

    // handle when a duck is hit, reducing the number of ducks
    public void DuckHit()
    {
        ducksLeft--; // Decrease the number of ducks left
        HUD.instance.setDucksLeft(ducksLeft); // update the HUD with the new number of ducks

        // when all ducks are hit and there are still beach balls left
        if (ducksLeft <= 0 && beachBallsLeft >= 0)
        {
            // stop the music and show the win screen
            if (backgroundMusic != null) backgroundMusic.Stop();
            youWinScreen.SetActive(true); 
            Time.timeScale = 0f; 
        }
        else
        {
            playerLauncher.hasLaunched = false; // reset the launcher if there are still ducks
        }
    }

    // pause the game and display the pause screen
    public void PauseGame()
    {
        pauseScreen.SetActive(true); // show the pause screen
        if (backgroundMusic != null) backgroundMusic.Pause(); // pause the background music
        Time.timeScale = 0f; 
        isPaused = true; 
    }

    // resume the game and hide the pause screen
    public void ResumeGame()
    {
        pauseScreen.SetActive(false); 
        if (backgroundMusic != null) backgroundMusic.UnPause(); // resume the background music
        Time.timeScale = 1f; 
        isPaused = false; 
    }

    // toggle between pausing and resuming the game
    public void TogglePause()
    {
        if (isPaused) ResumeGame(); 
        else PauseGame(); 
    }

    // load the main menu scene
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu"); // load the main menu scene
    }

    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // Quit the game if it's a build
#endif
    }

    // check if the player can still shoot based on the number of beach balls left
    public bool canShoot()
    {
        return beachBallsLeft > 0; //return true if there are beach balls left to shoot
    }
}