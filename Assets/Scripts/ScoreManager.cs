using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    private int score = 0;
    private int health = 100;
    private int coins = 0;
    private string stageName = "Stage";

    public PlayerController player;
    private GameControlller game;
    private UIController ui;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlller>();
        ui = GetComponent<UIController>();
    }

    public void AddScore(int amount){
        score += amount;
    }

    public void LoseHealth(float amount){
        int amountInt = Mathf.RoundToInt(amount);
        health -= amountInt;
        ui.UpdateHealth();
    }

    public void IncreaseCoins(int amount){
        coins += amount;
        ui.UpdateCoins();
    }

    public void SetStageName(string name){
        stageName = name;
        ui.UpdateStageName();
    }

    public int GetScore(){
        return score;
    }

    public int GetHealth(){
        return health;
    }

    public int GetCoins(){
        return coins;
    }

    public string GetStageName(){
        return stageName;
    }
}
