using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public const int itemLimit = 18;
    /*
    What each stage introduces
    Stage Level 0: Tomatoes
    Stage Level 1: Potatoes
    Stage Level 2: Poop
    Stage Level 3: Poison
    Stage Level 4: Toxic Waste
    Stage Level 5: Health falls faster
    */
    private int stage; //Stage Level is used to determine what plates to spawn and how often, as well as scoring
    private int actionsTaken; //Stage increments as the player takes more actions (eats, throws plates away, etc.)


    // Start is called before the first frame update
    void Start()
    {
        actionsTaken = 0;
        stage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(actionsTaken > stage*2 && stage < 5) {
            actionsTaken = 0;
            Debug.Log("Incrementing Stage");
            stage++;
            //Some stages add a game element when they're reached
            switch(stage)
            {
                case 2:
                    Debug.Log("Enter Garbage from stage left.");
                    break;
                case 4:
                    Debug.Log("Enter Toxic Barrel from stage right.");
                    break;
                case 5:
                    Debug.Log("Famine Mode Activated!");
                    break;
            }
        }
    }

    public int GetStage() {
        return stage;
    }

    public void TakeAction() {
        Debug.Log("Action Taken");
        actionsTaken++;
    }
}
