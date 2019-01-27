using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float cleanliness;
    public float gameTimer;

    private void Awake()
    {
        Instance = this;
        cleanliness = 1;
    }

    private void Start()
    {
        UIManager.Instance.SetGameTimer(gameTimer);
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;

        UIManager.Instance.UpdateScoreSlider(cleanliness);

        if (gameTimer <= 0)
        {
            //stop game
        }
    }

    public void ChangeCleanliness(float value)
    {
        cleanliness += value;
    }
}
