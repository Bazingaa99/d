using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private Slider health;
    private Text score;
    private Text stageName;
    private Text coins;

    private ScoreManager scoreManager;
    void Start(){
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
        stageName = GameObject.FindGameObjectWithTag("StageName").GetComponent<Text>();
        coins = GameObject.FindGameObjectWithTag("Coins").GetComponent<Text>();

        scoreManager = GetComponent<ScoreManager>();

        health.value = scoreManager.GetHealth() * 0.01f;
        stageName.text = scoreManager.GetStageName();
        coins.text = scoreManager.GetCoins().ToString();
    }

    public void UpdateHealth(){
        health.value = scoreManager.GetHealth() * 0.01f;
        if(health.value < 0.01){
            EndGame();
        }
    }

    public void UpdateCoins(){
        coins.text = scoreManager.GetCoins().ToString();
    }
    public void UpdateStageName(){
        stageName.text = scoreManager.GetStageName();
    }

    private void EndGame(){
        SceneManager.LoadScene("SampleScene");
    }
}
