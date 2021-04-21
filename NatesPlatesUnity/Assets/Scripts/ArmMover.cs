using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMover : MonoBehaviour
{
    //GameObject of the plate
    public SpriteRenderer plateSprite;
    public GameObject platePrefab;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    //The arm is either serving (moving towards table) or not (away from table)
    private bool serving = true;
    private PlateGenerator plateGenScript;

    float t;
    float timeToReachTarget;

     void Start()
     {
            //  startPos = endPos = transform.position;
            // transform.LookAt(endPos);
            // transform.right = endPos - transform.position;

            // SetDestination(endPos, 4.0f);
     }
     public void Init(Vector3 platePosition, float extraRotate, GameObject plateGenerator)
     {
         endPos = platePosition;
        plateGenScript = plateGenerator.GetComponent(typeof(PlateGenerator)) as PlateGenerator;

        //Turn arm to face the plate position
         transform.right = endPos - transform.position;

        //Set destination to move arm to in update
         SetDestination(endPos, 4.0f);
     }
     void Update() 
     {
            //Move to the set destination endPos
             t += Time.deltaTime/timeToReachTarget;
             transform.position = Vector3.Lerp(startPos, endPos, t);

             //Once the destination has been reached, place a plate and reverse
             if(transform.position == endPos) {
                 if(serving) {
                    serving = false;
                    //delete the arm's plate object
                    plateSprite.enabled = false;
                    PlacePlate(endPos);
                    SetDestination(startPos, 3.0f);
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

    //Create Plate GameObject
    public void PlacePlate(Vector3 position) 
    {
        GameObject plateObject = Instantiate(platePrefab, position, Quaternion.identity) as GameObject;
    }

    public void DestroyArm()
    {
        Destroy(gameObject);
    }
}
