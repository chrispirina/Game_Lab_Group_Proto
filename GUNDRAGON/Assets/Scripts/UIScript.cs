using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIScript : MonoBehaviour
{
    public int finalScore;
    public int finalHealth;
    public int finalAmmo;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI scoreText;
    public GameObject gameManager;
    public Image health1;
    public Image health2;
    public Image health3;
    
    public TextMeshProUGUI finalTime;
    public TextMeshProUGUI headshots;
    public TextMeshProUGUI heartshots;
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI finalScoreText;


    void Start ()
    {
        
        finalAmmo = 0;       
        finalScore = 0;
        gameManager.GetComponent<GameManager>();
	}
	
	
	void Update ()
    {
        if (gameManager.GetComponent<GameManager>().scoreAdd == true)
        {
            finalScore += gameManager.GetComponent<GameManager>().score;
            gameManager.GetComponent<GameManager>().scoreAdd = false;
            gameManager.GetComponent<GameManager>().score = 0;

        }

        finalAmmo = Player.ammo;
        finalHealth = Player.health;

        finalTime.text = ("Time Survived: " + GameManager.Instance.timeSurvived.ToString("00.0"));
        headshots.text = ("Headshots Achieved: " + Enemy.headShots.ToString());
        heartshots.text = ("Heartshots Achieved: " + Enemy.heartShots.ToString());
        enemiesKilled.text = ("Total Enemies Slayed: " + Enemy.enemiesKilled.ToString());
        finalScoreText.text = ("Final Score: " + finalScore.ToString());

        scoreText.text = finalScore.ToString();
        ammoText.text = finalAmmo.ToString();

        if (finalHealth >= 3)
        {
            health1.color = Color.green;
            health2.color = Color.green;
            health3.color = Color.green;
        }

        if (finalHealth == 2)
        {
            health1.color = Color.yellow;
            health2.color = Color.yellow;
            health3.color = Color.grey;
        }

        if (finalHealth == 1)
        {
            health1.color = Color.red;
            health2.color = Color.grey;
            health3.color = Color.grey;
        }

        if (finalHealth <= 0)
        {
            health1.color = Color.grey;
            health2.color = Color.grey;
            health3.color = Color.grey;
        }

       

    }

    

}
