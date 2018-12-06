using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            if (Enemy.amAttacking == true)
            {
                Player.publicPlayerHealth -= GetComponentInParent<Enemy>().Damage;                
                other.gameObject.GetComponent<Player>().playerWasHit = true;
                Debug.Log("Smacked player");
                Enemy.amAttacking = false;
            }
            else
                Debug.Log("Couldnt smack player");
        }
    }
}
