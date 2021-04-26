using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecepticalMovement : MonoBehaviour
{
    private bool movementActive = false;
    private bool comingIn = false;

    public int speed = 1;

    public int radius = 1;

    public Vector3 centerPosition;

    public bool movingRight;

    private Vector3 xMax;
    private Vector3 xMin;

    private void Start()
    {
        xMin = new Vector3(centerPosition.x - radius, centerPosition.y, centerPosition.z);
        xMax = new Vector3(centerPosition.x + radius, centerPosition.y, centerPosition.z);
    }

    private void Update()
    {
        if (comingIn)
        {
            if (Vector2.Distance(transform.position, centerPosition) > 0.01)
            {
                transform.position = Vector2.MoveTowards(transform.position, centerPosition, speed * Time.deltaTime);
            }
            else
            {
                comingIn = false;
                movementActive = true;
            }
        }

        if (movementActive)
        {
            if (Vector2.Distance(transform.position, xMax) < 0.01)
            {
                movingRight = false;
            }
            else if (Vector2.Distance(transform.position, xMin) < 0.01)
            {
                movingRight = true;
            }

            if (movingRight)
            {
                transform.position = Vector2.MoveTowards(transform.position, xMax, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, xMin, speed * Time.deltaTime);
            } 
        }
    }

    public void BringItIn()
    {
        comingIn = true;
    }
}
