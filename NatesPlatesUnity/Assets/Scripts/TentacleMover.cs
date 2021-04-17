using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMover : MonoBehaviour
{
    public string horizontal_axis_input;
    public string vertical_axis_input;

    public float horizontal_min;
    public float horizontal_max;

    public float vertical_min;
    public float vertical_max;

    public float speed = 8;

    private float h;
    private float v;

    public void Update()
    {
        h = Input.GetAxisRaw(horizontal_axis_input);
        v = Input.GetAxisRaw(vertical_axis_input);
    }

    public void FixedUpdate()
    {
        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        Vector3 new_position = transform.position + tempVect;

        if (
            new_position.x > horizontal_min
            && new_position.x < horizontal_max
        )
        {
            transform.position = new Vector3(new_position.x, transform.position.y, transform.position.z);
        }

        if (
            new_position.y > vertical_min
            && new_position.y < vertical_max
        )
        {
            transform.position = new Vector3(transform.position.x, new_position.y, transform.position.z);
        }
    }
}
