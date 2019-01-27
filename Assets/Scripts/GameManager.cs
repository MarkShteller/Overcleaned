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
    }

    private void Start()
    {
        UIManager.Instance.SetGameTimer(gameTimer);
        UIManager.Instance.UpdateDishwasherSlider(DishWasherCapacity);
        UIManager.Instance.UpdateWasherSlider(WasherCapacity);
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;

        UIManager.Instance.UpdateScoreSlider(cleanliness);

        if (gameTimer <= 0)
        {
            //stop game
            UIManager.Instance.ShowGameOverScreen();
            Time.timeScale = 0;
        }
    }

    public void AddDishwasherItem()
    {
        print("adding to dishwasher curr cap: "+ DishWasherCapacity);
        if (!isDishwasherFull)
        {
            DishWasherCapacity++;
            float percent = DishWasherCapacity / (float)DishWasherMaxCapacity;
            UIManager.Instance.UpdateDishwasherSlider(percent);
            if (percent == 1)
            {
                StartCoroutine(WashDishes());
            }
        }
    }

    public void AddWasherItem()
    {
        print("adding to washer curr cap: " + WasherCapacity);
        if (!isWasherFull)
        {
            WasherCapacity++;
            float percent = WasherCapacity / (float)WasherMaxCapacity;
            UIManager.Instance.UpdateWasherSlider(percent);
            if (percent == 1)
            {
                StartCoroutine(WashClothes());
            }
        }
    }

    private IEnumerator WashClothes()
    {
        isWasherFull = true;
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
    }

    private IEnumerator WashDishes()
    {
        isDishwasherFull = true;
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
    }

    public void ChangeCleanliness(float value)
    {
        cleanliness += value;
    }
}
