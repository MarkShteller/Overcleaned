using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas uiCanvas;
    public AudioSource menuMusic;

    public static MainMenu Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        mainMenuCanvas.enabled = true;
        uiCanvas.enabled = false;
    }


    public void StartGame()
    {
        mainMenuCanvas.enabled = false;
        uiCanvas.enabled = true;
        menuMusic.Stop();
        menuMusic.enabled = false;
    }
}
