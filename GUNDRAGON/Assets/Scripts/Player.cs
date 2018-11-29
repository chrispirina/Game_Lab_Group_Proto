using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float shootGun = 0;
    public float fireRate = 0.5f;
    public float gunDamage = 2.0f;

    float hitMelee = 0;
    public float meleeRate = 0.3f;
    public float meleeDamage = 10.0f;

    public float health = 100.0f;
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
    }
	
	// Update is called once per frame
	void Update ()
    {
        fwdGun = gunPos.TransformDirection(Vector3.forward);

        if (shootGun <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Shoot();
                shootGun = fireRate;
            }
        }
        if (shootGun > 0)
        {
            Debug.Log("Can't shoot yet");
            shootGun -= Time.deltaTime;
        } 
	}

    void MeleeAttack()
    {

    }

    public void Shoot()
    {
        playerAnimator.SetTrigger("GunShot");
        RaycastHit hit;
        if (Physics.Raycast(gunPos.position, fwdGun, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(gunPos.position, fwdGun * hit.distance, Color.green);
            Debug.Log("Hit Something");
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Enemy>().enemyHealth -= gunDamage;
                ScoreManager.Instance.CombatScore += ScoreManager.Instance.gunAttackScore * ScoreManager.Instance.comboModifier;
                ScoreManager.Instance.hitCount += 1;

            }
        }
    }

}
