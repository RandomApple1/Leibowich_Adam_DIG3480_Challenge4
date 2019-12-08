using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioClip musicClipOne;
    public AudioSource musicSource;
    public AudioClip musicClipTwo;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text gameByText;
    

    private bool gameOver;
    private bool restart;
    public int score;
    private int points;
    private int wave;
    public int level;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        gameByText.text = "";
        
        points = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        wave = 0;
        
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (level == 1)
                {
                    SceneManager.LoadScene("Paper Plane Shooter");
                }
                if (level == 3)
                {
                    SceneManager.LoadScene("Paper Plane Shooter 2");
                }
                if (level == 5)
                {
                    SceneManager.LoadScene("Paper Plane Shooter 3");
                }
                if (level == 6)
                {
                    SceneManager.LoadScene("Paper Plane Shooter");
                }
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (score >= 1700)
            {
                
                restartText.text = "Press 'T' to Play Again!!!";
                gameOverText.text = "You Win!!!";
                gameByText.text = "Game By: Adam Leibowich";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                restart = true;
                break;
            }
            for (int i = 0; i < hazardCount; i++)
            {     
                GameObject hazard = hazards[Random.Range(0, 0)];
                if (wave >= 1)
                {
                    hazard = hazards[Random.Range(0, 1)];
                    if (wave >= 3)
                    {
                        hazard = hazards[Random.Range(0, 2)];
                        if (wave >= 5)
                        {
                            hazard = hazards[Random.Range(0, 3)];
                            if (wave >= 7)
                            {
                                hazard = hazards[Random.Range(1, hazards.Length)];
                            }
                        }
                    }

                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                else
                {
                    if ( level == 1)
                    {
                        gameOverText.text = "Level 1";
                        gameByText.text = "Get 200 points to win!!!";
                        
                    }
                    if (level == 2)
                    {
                        gameOverText.text = "Level 2";
                        gameByText.text = "Get 500 points to win!!!";
                        scoreText.text = "Points: " + points;
                        points = 0;
                        
                    }
                    if (level == 4)
                    {
                        gameOverText.text = "Final Level";
                        gameByText.text = "Get 1000 points to win!!!";
                        scoreText.text = "Points: " + points;

                        points = 0;
                        
                    }
                    
                }
                yield return new WaitForSeconds(spawnWait);
            }
            if (level == 2)
            {
                wave = 3;
                level = level + 1;
            }
            if (level == 4)
            {
                wave = 5;
                level = level + 1;
            }
            if (gameOver)
            {
                restartText.text = "Press 'T' to Try Again";
                restart = true;
                break;
                
            }

            if (score >= 200)
            {
                gameOverText.text = "Level Complete!!!";
                gameByText.text = "";
                if (level == 1)
                {
                    yield return new WaitForSeconds(spawnWait);
                    SceneManager.LoadScene("Paper Plane Shooter 2");
                }
                

            }

            if (score >= 700)
            {
                gameOverText.text = "Level Complete!!!";
                gameByText.text = "";
                if (level == 3)
                {
                    yield return new WaitForSeconds(spawnWait);
                    SceneManager.LoadScene("Paper Plane Shooter 3");
                }
            }

            if (score >= 1700)
            {
                SceneManager.LoadScene("Paper Plane Shooter 4");
                
            }
            wave = wave + 1;
            gameOverText.text = "";
            gameByText.text = "";
            
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        points += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + points;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameByText.text = "Game By: Adam Leibowich";
        gameOver = true;
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
}