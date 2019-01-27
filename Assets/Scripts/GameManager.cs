using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int WasherCapacity;
    public int WasherMaxCapacity;
    public int DishWasherMaxCapacity;
    private int DishWasherCapacity;

    public bool isDishwasherFull;
    public bool isWasherFull;

    private bool _gameStarted;

    public float cleanliness;
    public float gameTimer;

    private void Awake()
    {
        Instance = this;
        cleanliness = 1;
        WasherCapacity = 0;
        DishWasherCapacity = 0;
        isDishwasherFull = false;
        isWasherFull = false;
        _gameStarted = false;
    }

    private void Start()
    {
        
    }

    public void StartGame()
    {
        UIManager.Instance.SetGameTimer(gameTimer);
        UIManager.Instance.UpdateDishwasherSlider(DishWasherCapacity);
        UIManager.Instance.UpdateWasherSlider(WasherCapacity);
        _gameStarted = true;
    }

    private void Update()
    {
        if(_gameStarted)
        {
            gameTimer -= Time.deltaTime;

            UIManager.Instance.UpdateScoreSlider(cleanliness);

            if (gameTimer <= 0)
            {
                //stop game
                UIManager.Instance.ShowGameOverScreen(cleanliness);
                AudioManager.Instance.StopMainMusic();
                Time.timeScale = 0;
            }

        }
    }

    public void AddDishwasherItem(Animator animator)
    {
        print("adding to dishwasher curr cap: "+ DishWasherCapacity);

        AudioManager audioManager = AudioManager.Instance;
        if (!audioManager.IsDishWasherFxPlaying())
        {
            audioManager.PlayDishWasherFx();
        }
        if (!isDishwasherFull)
        {
            DishWasherCapacity++;
            float percent = DishWasherCapacity / (float)DishWasherMaxCapacity;
            UIManager.Instance.UpdateDishwasherSlider(percent);
            if (percent == 1)
            {
                StartCoroutine(WashDishes(animator));
            }
        }
    }

    public void AddWasherItem(Animator animator)
    {
        print("adding to washer curr cap: " + WasherCapacity);
        AudioManager audioManager = AudioManager.Instance;
        if (!audioManager.IsWasherFxPlaying())
        {
            AudioManager.Instance.PlayWasherFx();
        }
        if (!isWasherFull)
        {
            WasherCapacity++;
            float percent = WasherCapacity / (float)WasherMaxCapacity;
            UIManager.Instance.UpdateWasherSlider(percent);
            if (percent == 1)
            {
                StartCoroutine(WashClothes(animator));
            }
        }
    }

    private IEnumerator WashClothes(Animator animator)
    {
        isWasherFull = true;
        animator.SetBool("Washing", isWasherFull);

        float maxTimer = 8;
        float washerCooldown = maxTimer;
        while (washerCooldown >= 0)
        {
            washerCooldown -= Time.deltaTime;
            UIManager.Instance.UpdateWasherSlider(washerCooldown / maxTimer);
            yield return null;
        }
        WasherCapacity = 0;
        isWasherFull = false;
        animator.SetBool("Washing", isWasherFull);
    }

    private IEnumerator WashDishes(Animator animator)
    {
        isDishwasherFull = true;
        animator.SetBool("Washing", isDishwasherFull);
        float maxTimer = 8;
        float dishwasherCooldown = maxTimer;
        
        while (dishwasherCooldown >= 0)
        {
            dishwasherCooldown -= Time.deltaTime;
            UIManager.Instance.UpdateDishwasherSlider(dishwasherCooldown / maxTimer);
            yield return null;
        }
        
        DishWasherCapacity = 0;
        isDishwasherFull = false;
        animator.SetBool("Washing", isDishwasherFull);
    }

    public void ChangeCleanliness(float value)
    {
        cleanliness += value;
        cleanliness = Math.Min(Math.Max(cleanliness, 0.0f), 100.0f);
    }
}
