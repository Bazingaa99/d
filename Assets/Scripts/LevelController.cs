using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private GameControlller game;
    private List<GameObject> bricks = new List<GameObject>();
    private List<GameObject> enemies;
    public int level;
    public int nextLevel;
    void Awake()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");

        game = gameObject.GetComponent<GameControlller>();
    }

    public void DestroyBrick(GameObject go){
        bricks.Remove(go);

        if(bricks.Count < 1){
            game.PrepareNewLevel(nextLevel);
        }
    }

    public void AddBrick(GameObject go){
        bricks.Add(go);
    }
}
