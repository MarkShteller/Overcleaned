using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider scoreSlider;
    public Slider washerSlider;
    public Slider dishWasherSlider;

    public static UIManager Instance;

    public Text GameTimerText;

    public Text titleDishwasher;
    public Text titleWasher;
    public Text titleTrashcan;
    public Text titleWindow;
    public Text titleCloset;
    public Text finalScore;

    public GameObject GameOverScreen;

    private float gameTimer;

    void Awake()
    {
        Instance = this;
        ToggleTitleDishwasher();
        ToggleTitleWasher();
        ToggletitleCloset();
        ToggletitleTrashcan();
        ToggletitleWindow();
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        GameTimerText.text = gameTimer.ToString("N0");
    }

    public void SetGameTimer(float time)
    {
        gameTimer = time;
    }

    public void UpdateDishwasherSlider(float value)
    {
        dishWasherSlider.value = value;
    }

    public void UpdateWasherSlider(float value)
    {
        washerSlider.value = value;
    }

    public void UpdateScoreSlider(float value)
    {
        scoreSlider.value = value;
    }

    public void ShowGameOverScreen(float score)
    {
        finalScore.text = (score * 100).ToString("N0") + " / 100";
        GameOverScreen.SetActive(true);
    }

    public void PlayAgainAction()
    {
        //reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleTitleDishwasher()
    {
        print("toggling dishwasher title");
        titleDishwasher.gameObject.SetActive(!titleDishwasher.gameObject.active);
    }
    public void ToggleTitleWasher()
    {
        titleWasher.gameObject.SetActive(!titleWasher.gameObject.active);
    }
    public void ToggletitleTrashcan()
    {
        titleTrashcan.gameObject.SetActive(!titleTrashcan.gameObject.active);
    }
    public void ToggletitleWindow()
    {
        titleWindow.gameObject.SetActive(!titleWindow.gameObject.active);
    }
    public void ToggletitleCloset()
    {
        titleCloset.gameObject.SetActive(!titleCloset.gameObject.active);
    }
}
