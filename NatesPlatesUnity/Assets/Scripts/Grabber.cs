using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public AudioClip schlorp;
    public bool notHoldingAnything = true;
    public bool gloved = false;
    public bool isRightTentacle;
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


    private void Start()
    {
        StartCoroutine("CheckIfHolding");
    }

    private IEnumerator CheckIfHolding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            notHoldingAnything = !transform.GetComponentInChildren<EdibleFood>()
                && !transform.GetComponentInChildren<ToxicItem>()
                && !transform.GetComponentInChildren<TrashItem>()
                && !transform.GetComponentInChildren<SinkItem>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gloved && notHoldingAnything)
        {
            if (collision.CompareTag("PlatedTomato"))
            {
                GrabItemOffPlate(tomato, collision.gameObject);
            }

            if (collision.CompareTag("PlatedPotato"))
            {
                GrabItemOffPlate(potato, collision.gameObject);
            }

            if (collision.CompareTag("PlatedLighter"))
            {
                GrabItemOffPlate(lighter, collision.gameObject);
            }

            if (collision.CompareTag("PlatedPoop"))
            {
                GrabItemOffPlate(poop, collision.gameObject);
            }

            if (collision.CompareTag("dirtyPlate"))
            {
                notHoldingAnything = false;
                collision.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "HeldItem";
                collision.gameObject.transform.position = grabLocation.position;
                collision.gameObject.transform.parent = gameObject.transform;
            }

            if (collision.CompareTag("GloveSpotLeft") && !isRightTentacle)
            {
                canRemoveGlove = false;
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
                PutOnGlove();
            }

            if (collision.CompareTag("GloveSpotRight") && isRightTentacle)
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

            if (canRemoveGlove && notHoldingAnything && collision.CompareTag("GloveSpotLeft") && !isRightTentacle)
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                RemoveGlove();
            }

            if (canRemoveGlove && notHoldingAnything && collision.CompareTag("GloveSpotRight") && isRightTentacle)
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                RemoveGlove();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GloveSpotLeft") && !isRightTentacle)
        {
            canRemoveGlove = true;
        }

        if (collision.CompareTag("GloveSpotRight") && isRightTentacle)
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
