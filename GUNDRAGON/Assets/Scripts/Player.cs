using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ElementType 
    {
        NONE,
        FIRE,
    }

    public ElementType weaponType = ElementType.NONE;

    float shootGun = 0;
    float gunOut = 0;
    public float fireRate = 0.2f;
    public float gunDamage = 2.0f;

    public float hitMelee = 0;
    public float meleeRate = 0.2f;
    public float meleeDamage = 10.0f;
    public static bool didSmack = false;

    public static bool didPause = false;

    public bool playerWasHit = false;

    public float health;
    public static float publicPlayerHealth = 100.0f;

    Transform gunPos;
    Transform meleePos;
    Vector3 fwdGun;
    Animator playerAnimator;

    // Use this for initialization
    void Start ()
    {
        meleePos = GameObject.FindGameObjectWithTag("MeleeHitbox").transform;
        gunPos = GameObject.FindGameObjectWithTag("GunEmitter").transform;
        playerAnimator = GetComponent<Animator>();
        health = publicPlayerHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (didPause == false)
        {
            if (health > publicPlayerHealth)
            {
                health = publicPlayerHealth;
            }
            fwdGun = gunPos.TransformDirection(Vector3.forward);

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("GunAim") == true && gunOut > 0)
            {
                gunOut -= Time.deltaTime;
            }

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("GunAim") == true && gunOut <= 0)
            {
                playerAnimator.SetTrigger("Holster");
                playerAnimator.ResetTrigger("Draw");
            }

            if (shootGun <= 0)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("GunAim") != true)
                    {
                        playerAnimator.ResetTrigger("Holster");
                        playerAnimator.SetTrigger("Draw");
                        gunOut = 2.0f;
                    }

                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("GunAim") == true)
                    {
                        gunOut = 1.0f;
                        Shoot();
                        shootGun = fireRate;
                    }

                }
            }
            if (shootGun > 0)
            {
                shootGun -= Time.deltaTime;
            }
            if (hitMelee <= 0)
            {
                playerAnimator.ResetTrigger("Attack");
                didSmack = false;
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit1toHit2") == true)
                {
                    playerAnimator.SetTrigger("HitToIdle");
                }

                if (Input.GetMouseButtonDown(0))
                {
                    MeleeAttack();
                    playerAnimator.ResetTrigger("HitToIdle");
                    hitMelee = meleeRate;
                }
            }
            if (hitMelee > 0)
            {
                didSmack = true;
                Debug.Log("Can't hit yet");
                hitMelee -= Time.deltaTime;

                if (Input.GetMouseButtonDown(0))
                {
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit1toHit2") == true || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit1") == true)
                    {
                        MeleeAttack();
                        playerAnimator.ResetTrigger("HitToIdle");
                        hitMelee = meleeRate;
                    }

                }
            }
        }       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (didPause == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                didPause = true;
            }

            else if (didPause == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                didPause = false;
            }

        }
    }

    void MeleeAttack()
    {
        if (PlayerMovement.didJump == true)
        {
            playerAnimator.SetTrigger("JumpAttack");
        }
        else if (PlayerMovement.didJump != true)
        {
            playerAnimator.SetTrigger("Attack");
        }
        
    }

    public void Shoot()
    {
        playerAnimator.SetTrigger("Shoot");
        RaycastHit hit;
        if (Physics.Raycast(gunPos.position, fwdGun, out hit, Mathf.Infinity))
        {
            Debug.Log("Did Shoot");
            Debug.DrawRay(gunPos.position, fwdGun * hit.distance, Color.green);
            if (hit.collider.CompareTag("Enemy"))
            {
                if (hit.transform.gameObject.GetComponent<Enemy>())
                {
                    hit.transform.gameObject.GetComponent<Enemy>().enemyHealth -= gunDamage;
                    hit.transform.gameObject.GetComponent<Enemy>().wasHit = true;
                }
                else if (hit.transform.gameObject.GetComponentInParent<Enemy>())
                {
                    hit.transform.gameObject.GetComponentInParent<Enemy>().enemyHealth -= gunDamage;
                    hit.transform.gameObject.GetComponentInParent<Enemy>().wasHit = true;
                }
                ScoreManager.Instance.CombatScore += ScoreManager.Instance.gunAttackScore * ScoreManager.Instance.comboModifier;
                ScoreManager.Instance.hitCount += 1;
                Debug.Log("Hit an Enemy");

            }
        }
    }

}
