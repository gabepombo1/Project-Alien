using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

public class Projectile : MonoBehaviour
{

    [SerializeField] public GameObject projectile;
    private float Speed = 2;
    private float Damage = 10;

    void Move()
    {
        Vector3 targetPosition = gameObject.GetComponent<Minion>().PickTarget(
            gameObject.GetComponent<VisionRange>().enemyTargets).transform.position;
        
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, Speed);
    }

    private void OnCollisionEnter(Collision other)
    {        
        Destroy(gameObject);
        float newHealth = other.gameObject.GetComponent<Minion>().health - Damage;
        other.gameObject.GetComponent<Minion>().health = newHealth;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
}
