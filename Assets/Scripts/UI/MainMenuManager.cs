using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : IModable
{
    public GameObject BOSD;

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

    protected override void AwakeInternal()
    {
    }

    protected override void OnEnableInternal()
    {
    }

    protected override void UpdateInternal()
    {
    }

    protected override void OnDisableInternal()
    {
    }

    protected override void OnDestroyInternal()
    {
    }
}
