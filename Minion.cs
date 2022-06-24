using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//COMBAT MECHANICS IDEA//
//the combat is done using a stream of different kinds of units since the invasion is done using a recycling method where when units die they can be brought back
//and their spirit can be used again, even fr another type of unit (some types make require more "spirits" than others)
//the units are kind of stupid, and they only target one person ina  fight until they die
//they could be little globs or weird lizard mini guys to shoe how they are inferior

//VISIONRANGE IDEA//
//need to have a VisionRange class attached to a circle around the unit that collects like 5 targets and analyzes them and sees which one it should attack
//there can be 5 targets total, and if one leaves the area, then they are now off the list and any unit that was not on the list is now on the list (randomly chosen or just have it be
//the closest one to the unit that owns the vision circle if there is more than one extra unit)

//SQUAD IDEA//
//it would also be cool to have the units automatically form mini squads that are balanced with a tank and a shooter and such\
//it'd be cool to make the units bouncy and have a visual effect like that Badland Brawl game
//have some creatures be "smart" or other traits, which makes their squad react faster when making decisions (smart) or boosts damage or something (flaming
//weapons or something)

//MEMORY IDEA//
//maybe have it have a memory of previous 3-5 enemy locations that left the VisionRange and then use that data to then make the squad's more intelligent
//by using the location data to help decide on movements/rotations for the squads or squad members
public class Minion : MonoBehaviour
{

    [SerializeField] private GameObject unit;
    [SerializeField] public string teamTag = "Red";
    //private int VisionRange = 40;
    [SerializeField] public string[] enemyTeams = {""};
    public Queue<GameObject> PreviousTargets;
    [SerializeField] public float health = 100;

    void Die()
    {
                
        Destroy(gameObject);
                
    }
    
    //use the stats/probability of hitting algorithm you made for RTS game
    enum UnitState
    {
            
            ATTACKING,
            WALKING,
            
    }
    
    void Attack(Queue<GameObject> enemyQueue)
    {

        GameObject currentTarget = enemyQueue.Peek();

        while (currentTarget.GetComponent<Minion>().health > 0)
        {

            Instantiate(gameObject.GetComponent<Projectile>().projectile, 
                gameObject.transform.position,
                gameObject.transform.rotation);

        }

    }
    
    void UnitStateMachine(UnitState unitState)
    {

        if (unitState == UnitState.ATTACKING)
        {
            
            PickTarget(gameObject.GetComponent<VisionRange>().enemyTargets);
            Attack(gameObject.GetComponent<VisionRange>().enemyTargets);
                        
        }

        /*switch (unitState)
        {
                case UnitState.ATTACKING:
                        Attack(enemyTargets);
        }*/
                
    }
    
    void SetTag(string inputTag)
    {

        gameObject.tag = inputTag;

    }
    
    //one function that queues the enemies and then a PickTarget that shoots the correct enemy
    //is this already done in the VisionRange script?
    /*public GameObject FindTarget()
    {

        GameObject currentUnit;
        
        for (int i = 0; i < enemyTeams.Length; i++)
        {
            
            
            
        }

        return currentUnit;

    }*/

    //chooses the right unit to attack
    public GameObject PickTarget(Queue<GameObject> inputQueue)
    {

        GameObject[] currentTargets = {};
        GameObject currentTarget;

        for (int i = 0; i < inputQueue.Count; i++)
        {

            currentTargets[i] = inputQueue.Dequeue();

        }

        currentTarget = currentTargets[0];

        //do i need tosubtract 1 from length here?
        for (int i = 0; i < currentTargets.Length; i++) {

            //Vector2 currentTargetDistance = currentTarget.transform.position - gameObject.transform.position;
            float currentTargetDistance = Vector3.Distance(currentTarget.transform.position, gameObject.transform.position);
            float nextTargetDistance = Vector3.Distance(currentTargets[i + 1].transform.position, gameObject.transform.position);
            
            if(currentTargetDistance > nextTargetDistance)
            {

                GameObject nextTarget = currentTargets[i + 1];
                GameObject placeholder = currentTargets[i];
                currentTargets[i] = nextTarget;
                currentTargets[i + 1] = currentTarget;
                currentTarget = nextTarget;

            } 
            
        }

        return currentTarget;

    }

    void FixedUpdate()
    {
        
        UnitStateMachine();

    }

    // Start is called before the first frame update
    void Start()
    {

        SetTag(teamTag);

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
    
}
