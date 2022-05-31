using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePoints : MonoBehaviour
{
    public int lifePoints { get; private set; }
    [SerializeField] private Text lifePointsText;

    public int highScore;
    [SerializeField] private Text highScoreText;
    
    private void Start()
    {
        lifePoints = 5;
        lifePointsText.text = "Life Points: " + lifePoints;
        highScoreText.text = "Highscore: " + highScore;
    }
    
    // the number of lives changes depending on 
    // the property of the prefab when it collides with objects 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Cherry"))
        {
            Destroy(col.gameObject);
            ++lifePoints;
            lifePointsText.text = "Life Points: " + lifePoints;

            highScore += 25;
            highScoreText.text = "Highscore: " + highScore;
        } 
        else if (col.gameObject.CompareTag("Apple"))
        {
            Destroy(col.gameObject);
            lifePoints += 2;
            lifePointsText.text = "Life Points: " + lifePoints;
            
            highScore += 30;
            highScoreText.text = "Highscore: " + highScore;
        }
        else if (col.gameObject.CompareTag("Strawberry"))
        {
            Destroy(col.gameObject);
            lifePoints += 3;
            lifePointsText.text = "Life Points: " + lifePoints;
            
            highScore += 50;
            highScoreText.text = "Highscore: " + highScore;
        }
        else if (col.gameObject.CompareTag("Orange"))
        {
            Destroy(col.gameObject);
            --lifePoints;
            lifePointsText.text = "Life Points: " + lifePoints;
            
            highScore -= 25;
            highScoreText.text = "Highscore: " + highScore;
        }
        
        else if (col.gameObject.CompareTag("FallingBlock"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
        
        else if (col.gameObject.CompareTag("SawHori"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
        
        else if (col.gameObject.CompareTag("SawVer"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
        
        else if (col.gameObject.CompareTag("SpikedBall"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
        
        else if (col.gameObject.CompareTag("Fire"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
        
        else if (col.gameObject.CompareTag("Spike"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
        else if (col.gameObject.CompareTag("Boss"))
        {
            --lifePoints;
            lifePointsText.text = "Life Points:" + lifePoints;

            highScore = -5;
            highScoreText.text = "Highscore: " + highScore;
        }
    }
}
