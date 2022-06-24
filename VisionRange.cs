using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
        
        [SerializeField] private Collider radius;
        [SerializeField] private string[] enemyTeamTags;
        [SerializeField] public Queue<GameObject> enemyTargets;

        //for when the unit spawns and there are enemies that will be in the radius without having collided with the unit's VisionRange
        void InitEnemyTargets()
        {

                Minion unit = gameObject.GetComponent<Minion>();
                string[] unitEnemyTeamArray = unit.enemyTeams;
                int unitEnemyTeamListLength = unitEnemyTeamArray.Length;

        }

        //this will just keep enqueueing forever, find a way to limit the queue to 5 targets
        void OnCollisionEnter(Collision other)
        {

                for (int i = 0; i < enemyTeamTags.Length; i++) {
                        
                        if (other.gameObject.CompareTag(enemyTeamTags[i]))
                        {

                                enemyTargets.Enqueue(other.gameObject);

                        }

                }

                if (enemyTargets.Count > 5)
                {

                        enemyTargets.Dequeue();

                }
                
        }

        // Start is called before the first frame update
        void Start()
        {

                enemyTeamTags = gameObject.GetComponent<Minion>().enemyTeams;

        }

        // Update is called once per frame
        void Update()
        {

                

        }
        
}