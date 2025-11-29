using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public static Canvas Instance;

    public Text p1Score;
    public int p1_score=0;

    public Text p2Score;
    public int p2_score=0;

    public Text playerWon;
    public GameObject gameOverScreen;

    public bool winner = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        gameOverScreen.ToString();
        p1Score.text = p1_score.ToString();
        p2Score.text = p2_score.ToString();
        
    }
 
    public void PlayerOneScore(int onescore)
    {
        p1_score += onescore;
        p1Score.text = p1_score.ToString();
    }

    public void PlayerTwoScore(int twoscore)
    {
        p2_score += twoscore;
        p2Score.text = p2_score.ToString();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        gameOverScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayerWon(string player)
    {
        playerWon.text = player;
    }

}
