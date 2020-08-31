using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    private float horizontal;

    [Header("Game Area variables")]
    private Transform rightBounds;
    private Transform leftBounds;

    private GameControlller game;
    private BallBehavior ball;
    private GameObject arrow;
    void Awake(){
        GameObject gameObject = GameObject.FindGameObjectWithTag("Right bounds");
        if(gameObject != null){
            rightBounds = gameObject.transform;
        }

        gameObject = GameObject.FindGameObjectWithTag("Left bounds");
        if(gameObject != null){
            leftBounds = gameObject.transform;
        }

        gameObject = GameObject.FindGameObjectWithTag("GameController");
        if(gameObject != null){
            game = gameObject.GetComponent<GameControlller>();
        }

        gameObject = GameObject.FindGameObjectWithTag("Ball");
        if(gameObject != null){
            ball = gameObject.GetComponent<BallBehavior>();
        }

        gameObject = GameObject.FindGameObjectWithTag("Arrow");
        if(gameObject != null){
            arrow = gameObject;
        }

        arrow.GetComponent<ArrowBehavior>().player = this.gameObject;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        

        if(!game.onGoing && Input.GetKeyDown(KeyCode.Space)){
            StartGame();
        }
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement(){
        float offset = transform.localScale.x / 1.5f;

        if(horizontal < 0 && (transform.position.x - offset) > (leftBounds.position.x)){
            transform.position += new Vector3(horizontal * speed, 0, 0);
        }
        if(horizontal > 0 && (transform.position.x + offset) < rightBounds.position.x){
            transform.position += new Vector3(horizontal * speed, 0, 0);
        }
    }

    private void StartGame(){
        GameObject gameObject = GameObject.FindGameObjectWithTag("Ball");

        if(gameObject != null){
            ball = gameObject.GetComponent<BallBehavior>();

            ball.LaunchBall(arrow.GetComponent<ArrowBehavior>().differenceX, arrow.GetComponent<ArrowBehavior>().differenceY, arrow.GetComponent<RectTransform>().localRotation.eulerAngles.z);
            game.onGoing = true;
            arrow.GetComponentInChildren<Image>().enabled = false;
        }

        
    }
}
