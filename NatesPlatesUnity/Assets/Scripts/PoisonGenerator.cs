using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGenerator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.ToString());
        GameObject other = collision.gameObject;
        if(other.tag == "Tomato" || other.tag == "PlatedTomato" || other.tag == "Potato" || other.tag == "PlatedPotato")
        {
            other.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
