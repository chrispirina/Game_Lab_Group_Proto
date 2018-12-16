using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private GameManager gameManager;

    public float playerMeleeDamage;
    public float playerGunDamage;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Player.didPause == true)
        {
            Time.timeScale = 0;
        }
        else if (Player.didPause == false)
        {
            Time.timeScale = 1;
        }
	}
}
