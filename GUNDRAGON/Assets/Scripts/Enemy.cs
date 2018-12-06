using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{

    public float enemyHealth = 100.0f;

    public bool wasHit = false;
    public float attackTimer;
    public float attackCooldown;
    public float attackRate = 2.0f;
    public float attackCooldownMax = 2.0f;
    public int bulletsShot = 0;

    public static bool amAttacking = false;
    public bool didAttack = false;
    public static bool enemyDead = false;

    public Animator enemyAnim;
    public Transform PlayerTransform;

    Vector3 PlayerDestination;
    private NavMeshAgent agent;
    public GameObject PlayerTarget;

    public bool isRanged = false;

    public GameObject enemyBullet;
    GameObject gunEmitter;
    Transform emitterPos;
    public float bulletForce = 10.0f;


    public float Damage = 10.0f;


    // Use this for initialization
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.enabled = true;
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = PlayerTarget.GetComponent<Transform>();
        gunEmitter = GameObject.FindGameObjectWithTag("EnemyGunEmitter");
        emitterPos = gunEmitter.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDestination = PlayerTransform.position;

        if (enemyHealth <= 0)
        {
            StartCoroutine(EnemyDeath());
        }


        if (agent.enabled == true && isRanged == false)
        {
            agent.destination = PlayerDestination;
            transform.LookAt(2 * agent.destination);
            if (agent.remainingDistance <= 2.0f)
            {
                if (amAttacking == false && didAttack == false)
                {
                    EnemyMeleeAttack();
                }
            }
        }

        if (agent.enabled == true && isRanged == true)
        {
            agent.destination = PlayerDestination;
            transform.LookAt(2 * agent.destination);
            if (agent.remainingDistance > 2.0f)
            {
                if (amAttacking == false && didAttack == false)
                {
                    EnemyShootAttack();
                }                
            }

        }
        

        if (attackCooldown > 0)
        {
            amAttacking = true;
            if (agent.enabled == true)
            {
                agent.isStopped = true;
            }
            attackCooldown -= Time.deltaTime;
        }
        else if (attackCooldown <= 0)
        {
            if (agent.enabled == true)
            {
                agent.isStopped = false;
            }
            amAttacking = false;
        }
        if (wasHit == true)
        {
            //play hit animation
            wasHit = false;
        }
        if (attackTimer > 0)
        {
            didAttack = true;
            attackTimer -= Time.deltaTime;
        }
        else if (attackTimer <= 0)
        {
            didAttack = false;
        }

        if (bulletsShot > 0)
        {
            shootBullet();
        }
    }

    public IEnumerator EnemyDeath()
    {
        agent.enabled = false;
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
        yield return new WaitForSeconds(3f);
        ScoreManager.Instance.CombatScore += 50 * ScoreManager.Instance.comboModifier;
        Destroy(gameObject);
    }

    void EnemyMeleeAttack()
    {
        attackCooldown = attackCooldownMax;
        attackTimer = attackRate;
        enemyAnim.SetTrigger("Enemy_Struck");
    }

    void EnemyShootAttack()
    {
        attackCooldown = attackCooldownMax;
        attackTimer = attackRate;
        enemyAnim.SetTrigger("Enemy_Shot");
    }

    void shootBullet()
    {
        GameObject tempBulletHandler;
        if (emitterPos != null)
        {
            tempBulletHandler = Instantiate(enemyBullet, emitterPos.position, emitterPos.rotation) as GameObject;
        }
    }

}
