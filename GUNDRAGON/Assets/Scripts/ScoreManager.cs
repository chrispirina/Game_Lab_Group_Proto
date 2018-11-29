using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;
    private ScoreManager scoreManager;

    public float LevelScore;
    public float CombatScore;
    public float comboModifier = 1.0f;
    public int meleeAttackScore = 20;
    public int gunAttackScore = 10;
    public int currentHitCount = 0;
    public int hitCount = 0;
    public float comboTimer = 0;
    public float maxComboTimer = 3.0f;

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

	// Use this for initialization
	void Start ()
    {
        LevelScore = 0;
        CombatScore = 0;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (hitCount > currentHitCount)
        {
            comboTimer = maxComboTimer;
        }
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        if (comboTimer <= 0)
        {
            hitCount = 0;
            currentHitCount = 0;
        }

        
    }

    void ComboModify()
    {
        if (hitCount > 0 && hitCount < 10)
        {
            comboModifier = 1.0f;
        }
        else if (hitCount >= 10 && hitCount < 20)
            
            {
                comboModifier = 1.2f;
            }

        else if (hitCount >= 20 && hitCount < 30)

        {
            comboModifier = 1.2f;
        }

        else if (hitCount >= 30 && hitCount < 40)

        {
            comboModifier = 1.3f;
        }

        else if (hitCount >= 40 && hitCount < 50)

        {
            comboModifier = 1.4f;
        }

        else if (hitCount >= 50 && hitCount < 60)

        {
            comboModifier = 1.5f;
        }

        else if (hitCount >= 60 && hitCount < 70)

        {
            comboModifier = 1.6f;
        }

        else if (hitCount >= 70 && hitCount < 80)

        {
            comboModifier = 1.7f;
        }

        else if (hitCount >= 80 && hitCount < 90)

        {
            comboModifier = 1.8f;
        }

        else if (hitCount >= 90 && hitCount < 100)

        {
            comboModifier = 1.9f;
        }

        else if (hitCount >= 100)

        {
            comboModifier = 2.0f;
        }
        else if (hitCount == 0)
        {
            comboModifier = 1;
        }
    }
}
