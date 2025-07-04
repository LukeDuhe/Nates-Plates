using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGenerator : MonoBehaviour
{
    public GameObject poisonProperty;
    private GameMaster gm;
    void Start() {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "Tomato" || other.tag == "PlatedTomato" || other.tag == "Potato" || other.tag == "PlatedPotato")
        {
            if(!other.transform.Find("PoisonProperty") && !other.transform.Find("PoisonProperty(Clone)")) {
                Debug.Log("Applying Poison");
                other.GetComponent<SpriteRenderer>().color = Color.green;
                GameObject prop = Instantiate(poisonProperty, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                prop.GetComponent<Transform>().parent = other.transform;
                gm.TakeAction();
            }

        }
    }
}
