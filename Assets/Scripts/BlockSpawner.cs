using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject block;
    void Awake()
    {
        GameObject gameObject = Instantiate(block, transform.position, Quaternion.identity) as GameObject;
        gameObject.transform.parent = transform.parent;
        Destroy(this.gameObject);
    }
}
