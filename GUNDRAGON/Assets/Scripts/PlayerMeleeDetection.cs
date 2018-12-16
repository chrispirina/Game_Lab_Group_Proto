using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeDetection : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit enemy");
            if (Player.didSmack == true)
            {
                if(other.gameObject.GetComponent<Enemy>())
                {
                    other.gameObject.GetComponent<Enemy>().enemyHealth -= GameManager.Instance.playerMeleeDamage;
                    other.gameObject.GetComponent<Enemy>().wasHit = true;
                }
                else if (other.gameObject.GetComponentInParent<Enemy>())
                {
                    other.gameObject.GetComponentInParent<Enemy>().enemyHealth -= GameManager.Instance.playerMeleeDamage;
                    other.gameObject.GetComponentInParent<Enemy>().wasHit = true;
                }
                
                ScoreManager.Instance.hitCount += 1;
                ScoreManager.Instance.CombatScore += (ScoreManager.Instance.meleeAttackScore * ScoreManager.Instance.comboModifier);
                Debug.Log("Smacked an Enemy");
            }
            else
                Debug.Log("Couldnt smack enemy");
        }
    }
}
