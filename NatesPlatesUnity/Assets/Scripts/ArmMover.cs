using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMover : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    //The arm is either serving (moving towards table) or not (away from table)
    private bool serving = true;

    float t;
    float timeToReachTarget;

     void Start()
     {
            //  startPos = endPos = transform.position;
            // transform.LookAt(endPos);
            // transform.right = endPos - transform.position;

            // SetDestination(endPos, 4.0f);
     }
     public void Init(Vector3 platePosition)
     {
         endPos = platePosition;

         transform.right = endPos - transform.position;
         SetDestination(endPos, 4.0f);
     }
     void Update() 
     {
             t += Time.deltaTime/timeToReachTarget;
             transform.position = Vector3.Lerp(startPos, endPos, t);
             if(transform.position == endPos) {
                 if(serving) {
                    serving = false;
                    SetDestination(startPos, 4.0f);
                 }
                 else {
                     DestroyArm();
                 }
             }
     }
     public void SetDestination(Vector3 destination, float time)
     {
            t = 0;
            startPos = transform.position;
            timeToReachTarget = time;
            endPos = destination; 
     }

    public void DestroyArm()
    {
        Destroy(gameObject);
    }
}
