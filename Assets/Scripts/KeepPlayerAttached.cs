using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayerAttached : MonoBehaviour
{
    private GameObject player;
    private bool playerFound = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPlayer());
    }

    private IEnumerator GetPlayer(){
        yield return new WaitForSeconds(0.2f);
        while(!playerFound){
            player = GameObject.FindGameObjectWithTag("Player");
            if(player != null){
                playerFound = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerFound && player.transform.position.y != transform.position.y){
            player.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            Debug.Log("What the hell man!");
        }
    }
}
