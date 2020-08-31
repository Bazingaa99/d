using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private GameControlller game;
    private ScoreManager scoreManager;
    public GameObject explosionAnim;
    private bool ballInstantiated;
    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
        if(gameObject != null){
            game = gameObject.GetComponent<GameControlller>();
        }

        gameObject = GameObject.FindGameObjectWithTag("Canvas");
        if(gameObject != null){
            scoreManager = gameObject.GetComponent<ScoreManager>();
        }
    }

    void Update()
    {
        
    }

    private IEnumerator WaitForBallDestroy(){
        while(!ballInstantiated){
            yield return new WaitForSeconds(0.05f);
            if(!GameObject.FindGameObjectWithTag("Ball")){
                game.SetBall();
                ballInstantiated = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ball")){
            scoreManager.LoseHealth(10f);
            Instantiate(explosionAnim, new Vector3( other.gameObject.transform.position.x, 
                                                    other.gameObject.transform.position.y - 0.25f, 
                                                    other.gameObject.transform.position.z), Quaternion.identity);
            Destroy(other.gameObject, 0.05f);
            ballInstantiated = false;
            StartCoroutine(WaitForBallDestroy());
        }
    }
}
