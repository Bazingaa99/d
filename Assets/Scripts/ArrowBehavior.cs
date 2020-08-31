using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    private RectTransform rect;

    [HideInInspector]
    public float differenceY;
    [HideInInspector]
    public float differenceX;
    void Awake(){
        rect = GetComponent<RectTransform>();

        GameObject gameObject = GameObject.FindGameObjectWithTag("Canvas");
        transform.SetParent(gameObject.transform);
    }
    void Update(){
        FollowPlayer();
        FaceMouse();
    }

    private void FaceMouse(){
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

        Vector3 difference = target - player.transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        if(rotationZ < 160 && rotationZ > 20){
            differenceX = difference.x;
            differenceY = difference.y;

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    private void FollowPlayer(){
        if(player != null){
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z));

            rect.anchorMin = viewportPoint;  
            rect.anchorMax = viewportPoint;
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
