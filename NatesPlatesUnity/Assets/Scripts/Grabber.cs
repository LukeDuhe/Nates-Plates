using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public AudioClip schlorp;
    public bool notHoldingAnything = true;
    public bool gloved = false;
    private bool canRemoveGlove = false;

    public Transform grabLocation;

    public SpriteRenderer gloveRenderer;

    public GameObject poisonProperty;

    public GameObject tomato;
    public GameObject potato;
    public GameObject toxicWaste;
    public GameObject lighter;
    public GameObject poop;

    public GameObject dirtyPlate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gloved)
        {
            if (notHoldingAnything && collision.CompareTag("PlatedTomato"))
            {
                GrabItemOffPlate(tomato, collision.gameObject);
            }

            if (notHoldingAnything && collision.CompareTag("PlatedPotato"))
            {
                GrabItemOffPlate(potato, collision.gameObject);
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

            if (notHoldingAnything && collision.CompareTag("GloveSpot"))
            {
                canRemoveGlove = false;
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
                PutOnGlove();
            }
        }
        else
        {
            if (notHoldingAnything && collision.CompareTag("PlatedToxicWaste"))
            {
                GrabItemOffPlate(toxicWaste, collision.gameObject);
            }

            if (canRemoveGlove && notHoldingAnything && collision.CompareTag("GloveSpot"))
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                RemoveGlove();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GloveSpot"))
        {
            canRemoveGlove = true;
        }
    }

    private void PutOnGlove()
    {
        gloved = true;
        gloveRenderer.enabled = true;
    }

    private void RemoveGlove()
    {
        gloved = false;
        gloveRenderer.enabled = false;
    }

    private void GrabItemOffPlate(GameObject item, GameObject collidingObject)
    {
        GetComponent<AudioSource>().PlayOneShot(schlorp);
        notHoldingAnything = false;
        
        bool poison = collidingObject.transform.Find("PoisonProperty") || collidingObject.transform.Find("PoisonProperty(Clone)");

        Instantiate(dirtyPlate, collidingObject.transform.position, collidingObject.transform.rotation);
        Destroy(collidingObject);
        GameObject new_item = Instantiate(item, grabLocation.position, grabLocation.rotation);

        Debug.Log("Picking up item, poison = " + poison);
        if(poison) {
            new_item.GetComponent<SpriteRenderer>().color = Color.green;
            GameObject prop = Instantiate(poisonProperty, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            prop.GetComponent<Transform>().parent = new_item.transform;
        }

        new_item.transform.parent = gameObject.transform;
    }
}
