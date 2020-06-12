using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelByIndex(int levelIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevelByName(string levelName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void printHi()
    {
        Debug.Log("Hi");
    }
}
