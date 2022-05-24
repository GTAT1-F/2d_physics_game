using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LivePoints playerHealth;
    [SerializeField] private Boss boss;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PauseMenu pauseMenu;

    private bool isGameOver;
    private bool playerVictory;
    private bool hasBeenEnded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForGameEnd();
        if (isGameOver && !hasBeenEnded) EndGame();
    }

    private void EndGame()
    {
        // Save the score
        scoreManager.AddCurrentScore();

        // Show Game Over screen
        // pauseMenu.Pause();

        hasBeenEnded = true;
    }

    private void CheckForGameEnd()
    {
        // Check player's health
        if (playerHealth.livePoints <= 0)
        {
            isGameOver = true;
            playerVictory = false;
        }
        // Check boss's health
        if (boss.Health <= 0)
        {
            isGameOver = true;
            playerVictory = true;
        }
    }
}
