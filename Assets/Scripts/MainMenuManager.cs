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

    public void AddModUI()
    {

    }

    public void LoadLevelByIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelByNameAndSave(string levelName)
    {
        // do stuff here

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
