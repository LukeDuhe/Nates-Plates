using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Sink"))
        {
            Destroy(gameObject);
        }
    }
    
}
