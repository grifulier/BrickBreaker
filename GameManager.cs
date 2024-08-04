using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public InputField highScoreInput;
    public bool gameOver = false;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public GameObject escapePanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;

        if (PlayerPrefs.GetInt("Music") == 1) {
            mainCamera.GetComponent<AudioSource>().Play();
        } else {
            mainCamera.GetComponent<AudioSource>().Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            EscapeMenu();
        }
    }

    public void UpdateLives(int changeInLives) {
        lives += changeInLives;

        // check for no lives left and trigger end of game
        if (lives <= 0) {
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "Score: " + score;
    }

    public void UpdateNumberOfBricks() {
        numberOfBricks--;
        if (numberOfBricks <= 0) {
            if (currentLevelIndex >= levels.Length - 1) {
                GameOver();
            }else {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                mainCamera.GetComponent<AudioSource>().Stop();
                gameOver = true;
                Invoke("LoadLevel", 3f);

            }
        }
    }

    void LoadLevel() {
        if (PlayerPrefs.GetInt("Music") == 1) {
            mainCamera.GetComponent<AudioSource>().Play();
        } else {
            mainCamera.GetComponent<AudioSource>().Pause();
        }
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }

    void GameOver() {
        gameOver = true;
        gameOverPanel.SetActive(true);
        mainCamera.GetComponent<AudioSource>().Stop();
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if (score > highScore) {
            PlayerPrefs.SetInt("HIGHSCORE", score);

            highScoreText.text = "New High Score!  " + "\n" + "Enter your name below";
            highScoreInput.gameObject.SetActive(true);

        }else {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "´s high score is " + highScore + "\n" + "Can you beat it?";
        }
    }

    void EscapeMenu() {
        gameOver = true;
        escapePanel.SetActive(true);
    }

    public void Continue() {
        gameOver = false;
        escapePanel.SetActive(false);
    }

    public void NewHighScore() {
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "Congratulations " + highScoreName + "\n" + "Your new high score is " + score;

    }

    public void PlayAgain() {
        SceneManager.LoadScene("Main");
    }

    public void Quit() {
        SceneManager.LoadScene("Menu");
    }
}
