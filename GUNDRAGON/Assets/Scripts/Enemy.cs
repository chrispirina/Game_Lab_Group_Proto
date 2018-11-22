using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public bool enemyIsHit = false;
    public bool enemyHeadIsHit = false;
    public bool enemyHeartIsHit = false;
    public bool enemyDead = false;
    public Animator enemyAnim;
    public Transform PlayerTransform;
    Vector3 PlayerDestination;
    private NavMeshAgent agent;
    public GameObject PlayerTarget;
    public static bool amAttacking = false;
    public static int enemiesKilled;
    public static int headShots;
    public static int heartShots;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.enabled = true;
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = PlayerTarget.GetComponent<Transform>();
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        PlayerDestination = PlayerTransform.position;
        

        if (agent.enabled == true)
        {
            agent.destination = PlayerDestination;
        }

        if (enemyIsHit == true)
        {
            enemyDead = true;
            enemiesKilled += 1;
            enemyIsHit = false;
        }

        if (enemyHeadIsHit == true)
        {
            enemyDead = true;
            headShots += 1;
            enemiesKilled += 1;
            enemyHeadIsHit = false;
        }

        if (enemyHeartIsHit == true)
        {
            enemyDead = true;
            heartShots += 1;
            enemiesKilled += 1;
            enemyHeartIsHit = false;
        }

        if (enemyDead == true)
        {
            StartCoroutine(EnemyDeath());
        }

        if (enemyDead != true && agent.remainingDistance < 1.5f)
        {
            StartCoroutine(EnemyAttack());
        }

    }

    public IEnumerator EnemyDeath()
    {
        agent.enabled = false;
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        enemyAnim.SetTrigger("isDead");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
       

    }

    public IEnumerator EnemyAttack()
    {
        amAttacking = true;       
        enemyAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(4f);
        amAttacking = false;
        

    }


}
