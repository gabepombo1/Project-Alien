using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{

    [SerializeField] private GameObject unit;
    [SerializeField] public string teamTag = "Red";
    [SerializeField] public string[] enemyTeams = {""};
    public Queue<GameObject> PreviousTargets;
    [SerializeField] public float health = 100;

    void Die()
    {                
        Destroy(gameObject);                
    }
    
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
    }
    
    void SetTag(string inputTag)
    {
        gameObject.tag = inputTag;
    }

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

        for (int i = 0; i < currentTargets.Length; i++) {
        
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
