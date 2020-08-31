using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private ScoreManager score;
    private LevelController level;
    [SerializeField]
    private int health;
    private int startingHealth;

    [SerializeField]
    private SpriteRenderer sprite;

    private float dissolveAmount = 1;
    private bool isAppearing = true;
    private bool isDissolving = false;
    public float dissolveSpeed;
    private Color burningOrange = new Vector4 (1.988f, 0.438f, 0.438f, 1.0f);
    void Awake()
    {
        startingHealth = health;

        GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
        if(gameObject != null){
            score = gameObject.GetComponent<ScoreManager>();
        }

        gameObject = GameObject.FindGameObjectWithTag("Level");
        if(gameObject != null){
            level = gameObject.GetComponent<LevelController>();
        }

        sprite = GetComponent<SpriteRenderer>();

        sprite.material.SetFloat("_DissolveAmount", dissolveAmount);

        level.AddBrick(this.gameObject);

        StartCoroutine(StartAppearing());
    }

    private IEnumerator StartAppearing(){
        float random = Random.Range(0, 1.4f);
        
        yield return new WaitForSeconds(random);
        
        StartCoroutine(Appear());
    }

    private IEnumerator Appear(){
        while(isAppearing){
            yield return new WaitForSeconds(0);
            dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
            sprite.material.SetFloat("_DissolveAmount", dissolveAmount);

            if(dissolveAmount < 0.01){
                isAppearing = false;
            }
        }
    }

    private IEnumerator Dissolve(float amount, bool destroy){
        while(isDissolving){
            yield return new WaitForSeconds(0);
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime + dissolveSpeed);
            sprite.material.SetFloat("_DissolveAmount", dissolveAmount);

            if(dissolveAmount > amount){
                isDissolving = false;
                if(destroy){
                    level.DestroyBrick(gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void TakeDamage(){
        health--;
    }

    private void ChangeState(){
        switch(health){
            case 1:
                StartCoroutine(Dissolve(0.5f, false));
                break;
            case 2:
                StartCoroutine(Dissolve(0.45f, false));
                break;
            case 3:
                StartCoroutine(Dissolve(0.4f, false));
                break;
            default:
                Debug.Log("Somethings wrong, I can feel it.");
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ball")){
            sprite.material.SetColor("_DissolveColor", burningOrange);
            isDissolving = true;
            TakeDamage();
            if(health > 0){
                ChangeState();
            }else{
                score.AddScore(15 * startingHealth);
                score.IncreaseCoins(2 * startingHealth);
                GetComponent<BoxCollider2D>().enabled = false;
                
                StartCoroutine(Dissolve(0.99f, true));
            }
        }
    }
}
