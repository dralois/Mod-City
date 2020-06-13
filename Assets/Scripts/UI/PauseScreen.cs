/*
 * written by Felix Völk
 * 
 * Script for UI in the playing game, changes Timescale, displays a HUD when slowed and provides a slider
 * 
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : IModable
{
    // Start is called before the first frame update
    public float timepause;
    private bool paused = false;
    public GameObject canvas;
    private InputHandler inputHandler;

    private void Cancel_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
            Pause();
    }

    protected override void Awake()
    {
        inputHandler = new InputHandler();
        inputHandler.Level.Cancel.performed += Cancel_performed;
        inputHandler.Level.Cancel.Enable();
        canvas.SetActive(false);
        base.Awake();
    }

    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = timepause;
            canvas.SetActive(true);
            paused = true;
        }
        else
        {
            Time.timeScale = 1f;
            canvas.SetActive(false);
            paused = false;
        }
    }

    public void LoadLevelByIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
