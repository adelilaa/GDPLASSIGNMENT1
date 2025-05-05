using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool GameIsOver = false;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    public TextMeshProUGUI shotsText;
    public TextMeshProUGUI targetsText;
    public TextMeshProUGUI elevationText; // New UI element for elevation

    private int shotsLeft = 10;
    private int targetsLeft = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameIsOver = false;
        UpdateUI();
    }

    void Update()
    {
        if (GameIsOver)
            return;

        if (shotsLeft <= 0 && targetsLeft > 0)
        {
            EndGame();
        }

        if (targetsLeft <= 0)
        {
            CompleteLevel();
        }
    }

    public void UseShot()
    {
        if (shotsLeft > 0)
        {
            shotsLeft--;
            UpdateUI();
        }
    }

    public void TargetHit()
    {
        if (targetsLeft > 0)
        {
            targetsLeft--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        shotsText.text = "Shots Left: " + shotsLeft;
        targetsText.text = "Targets Left: " + targetsLeft;
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    void CompleteLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }

    // Method to update the elevation UI
    public void UpdateElevation(float elevation)
    {
        elevationText.text = "Elevation: " + elevation.ToString("F1");
        print("Elevation: " + elevation.ToString("F1"));
    }
}