using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private int score;


    public void UpdateScore(int pointValue)
    {
        score += pointValue;
        scoreText.text = score.ToString();
    }
    
    public void panelManager(GameObject screen)
    {
        screen.SetActive(true);
        Time.timeScale = 0;
    }


    public void ChangeScene(int sceneNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }
}
