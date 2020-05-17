using System;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUIController : MonoBehaviour
{
    public GlobalSceneState sceneState;
    public GameObject countDown;
    public GameObject pauseButton;
    public GameObject continueButton;
    public GameObject restartButton;
    public GameObject exitButton;
    
    private float elapsedSecondsBeforeStart;
    private Text countDownText;

    public void Start()
    {
        countDownText = countDown.GetComponentInChildren<Text>();
        sceneState.OnSceneStateUpdated += OnSceneStateUpdated;
        elapsedSecondsBeforeStart = sceneState.startDelay;
    }

    public void Update()
    {
        if (sceneState.CurrentState == SceneState.Beginning)
        {
            countDownText.text = elapsedSecondsBeforeStart.ToString("#");
            elapsedSecondsBeforeStart -= Time.deltaTime;
            if (elapsedSecondsBeforeStart <= 0)
            {
                elapsedSecondsBeforeStart = sceneState.startDelay;
                sceneState.CurrentState = SceneState.Active;
            }
        }
    }

    public void OnClickPause()
    {
        sceneState.CurrentState = SceneState.Pause;
    }
    
    public void OnClickContinue()
    {
        sceneState.CurrentState = SceneState.Active;
    }
    
    public void OnClickRestart()
    {
        sceneState.CurrentState = SceneState.Beginning;
    }
    
    public void OnClickExit()
    {
        Application.Quit();
    }
    
    private void OnSceneStateUpdated(SceneState newState)
    {
        switch (newState)
        {
            case SceneState.Beginning:
                countDown.SetActive(true);
                pauseButton.SetActive(false);
                exitButton.SetActive(false);
                continueButton.SetActive(false);
                restartButton.SetActive(false);
                break;
            case SceneState.Active:
                countDown.SetActive(false);
                pauseButton.SetActive(true);
                exitButton.SetActive(false);
                continueButton.SetActive(false);
                restartButton.SetActive(false);
                break;
            case SceneState.Pause:
                countDown.SetActive(false);
                pauseButton.SetActive(false);
                exitButton.SetActive(true);
                continueButton.SetActive(true);
                restartButton.SetActive(true);
                break;
            case SceneState.GameOver:
                countDown.SetActive(false);
                pauseButton.SetActive(false);
                exitButton.SetActive(true);
                continueButton.SetActive(false);
                restartButton.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
