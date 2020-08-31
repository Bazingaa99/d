using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private CircleCollider2D box;
    public TrailRenderer trail;
    private float dissolveAmount = 0;
    private SpriteRenderer sprite;
    private bool isDissolving = true;
    void Awake()
    {
        trail.enabled = false;
        
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        transform.parent = gameObject.transform;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<CircleCollider2D>();
    }

    public void DestroyBall(){
        Destroy(gameObject);
    }

    public void DissolveBall(){
        box.enabled = false;
        StartCoroutine(Dissolve());
    }

    public void LaunchBall(float x, float y, float angle){
        transform.parent = null;
        Vector2 destination = new Vector2(x, y);
        destination.Normalize();
        rb.velocity = destination * speed;
        trail.enabled = true;
    }

    public IEnumerator Dissolve(){
        yield return new WaitForSeconds(0);
        while(isDissolving){
            yield return new WaitForSeconds(0);
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime);
            sprite.material.SetFloat("_DissolveAmount", dissolveAmount);
            rb.velocity.Set(rb.velocity.x - 1, rb.velocity.y - 1);
            Debug.Log("Dissolve amount: " + dissolveAmount);

            if(dissolveAmount > 0.99){
                isDissolving = false;
                Destroy(gameObject);
            }
        }
    }
}
