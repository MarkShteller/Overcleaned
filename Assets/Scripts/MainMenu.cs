using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            LoadScene();
        }
    }
    
    public void LoadScene()
    {
        SceneManager.LoadScene("Main");
    }
}
