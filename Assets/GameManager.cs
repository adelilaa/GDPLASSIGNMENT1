using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false; // Static variable to check if the game is over

    public GameObject gameOverUI; // UI element to display when the game is over
    public GameObject completeLevelUI; // UI element to display when the level is completed

    public TextMeshProUGUI shotsText; // UI text to display remaining shots
    public TextMeshProUGUI targetsText; // UI text to display remaining targets

    private int shotsLeft = 10;
    private int targetsLeft = 5;

     void Start()
    {
        GameIsOver = false;
        UpdateUI();
    }
     void Update()
    {
        if (GameIsOver)
            return;

        // Example condition to end the game if shots run out
        if (shotsLeft <= 0 && targetsLeft > 0)
        {
            EndGame();
        }

        // Example condition to complete the level if all targets are hit
        if (targetsLeft <=0)
        {
            CompleteLevel();
        }
    }

    // Method to reduce the number of shots left
    public void UseShot()
    {
        if (shotsLeft > 0)
        {
            shotsLeft--;
            UpdateUI();

        }
    }

    // Method to reduce the numbr of targets left
    public void TargetHit()
    {
        if (targetsLeft > 0)
        {
            targetsLeft--;
            UpdateUI();

        }

    }

    // Method to update the UI elements
    void UpdateUI()
    {
        shotsText.text = "Shots Left: " + shotsLeft;
        targetsText.text = "Targets Left; " + targetsLeft;

    }

    // Method to handle game over scenario
    void EndGame ()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    // Method to handle level completion scenario
    void CompleteLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
