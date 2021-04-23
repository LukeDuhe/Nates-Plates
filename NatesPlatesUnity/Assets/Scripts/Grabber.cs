using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public bool notHoldingAnything = true;

    public Transform grabLocation;

    public GameObject tomato;
    public GameObject potato;
    public GameObject toxicWaste;
    public GameObject lighter;
    public GameObject poop;

    public GameObject dirtyPlate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notHoldingAnything && collision.CompareTag("PlatedTomato"))
        {
            GrabItemOffPlate(tomato, collision.gameObject);
        }

        if (notHoldingAnything && collision.CompareTag("PlatedPotato"))
        {
            GrabItemOffPlate(potato, collision.gameObject);
        }

        if (notHoldingAnything && collision.CompareTag("PlatedToxicWaste"))
        {
            GrabItemOffPlate(toxicWaste, collision.gameObject);
        }

        if (notHoldingAnything && collision.CompareTag("PlatedLighter"))
        {
            GrabItemOffPlate(lighter, collision.gameObject);
        }

        if (notHoldingAnything && collision.CompareTag("PlatedPoop"))
        {
            GrabItemOffPlate(poop, collision.gameObject);
        }

        if (notHoldingAnything && collision.CompareTag("dirtyPlate"))
        {
            notHoldingAnything = false;
            collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "HeldItem";
            collision.gameObject.transform.position = grabLocation.position;
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void GrabItemOffPlate(GameObject item, GameObject collidingObject)
    {
        notHoldingAnything = false;
        Instantiate(dirtyPlate, collidingObject.transform.position, collidingObject.transform.rotation);
        Destroy(collidingObject);
        GameObject new_item = Instantiate(item, grabLocation.position, grabLocation.rotation);
        new_item.transform.parent = gameObject.transform;
    }
}
