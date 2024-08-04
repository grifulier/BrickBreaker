using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{
    public Text highScoreText;
    public Toggle musicToggle;

    void Start() {
        PlayerPrefs.SetInt("Music", 1);
        if (PlayerPrefs.GetString("HIGHSCORENAME") != "") {
        highScoreText.text = "Highest score by " + PlayerPrefs.GetString("HIGHSCORENAME") + ": " + PlayerPrefs.GetInt("HIGHSCORE");
        }
    }

    void Update() {
        
    }

    public void UpdateMusic() {
        if (PlayerPrefs.GetInt("Music") == 0) {
            PlayerPrefs.SetInt("Music", 1);
            GetComponent<AudioSource>().Play();
        } else if (PlayerPrefs.GetInt("Music") == 1){
            PlayerPrefs.SetInt("Music", 0);
            GetComponent<AudioSource>().Pause();
        }
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void StartGame() {
        SceneManager.LoadScene("Main");
    }
}
