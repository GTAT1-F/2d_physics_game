using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private LifePoints lp;
    [SerializeField] private Boss boss;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;

    private bool isGameOver;
    private bool playerVictory;
    private bool hasBeenEnded;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    // Update is called once per frame
    void Update()
    {
        CheckForGameEnd();
        if (isGameOver && !hasBeenEnded)
        {
            EndGame();
        }
        if (isGameOver)
        {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    private void EndGame()
    {
        // Save the score
        scoreManager.AddCurrentScore();
        hasBeenEnded = true;
    }

    private void CheckForGameEnd()
    {
        // Check player's health
        if (lp.lifePoints <= 0)
        {
            isGameOver = true;
            playerVictory = false;
            player.GetComponent<PlayerDeath>().Die();
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Dying"))
                // displays game over screen
                gameOverPanel.SetActive(true);
            }
        // Check boss's health
        if (boss.Health <= 0)
        {
            lp.highScore += 50;
            isGameOver = true;
            playerVictory = true;
            gameWinPanel.SetActive(true);
        }
    }

}
