using System;
using UnityEngine;

public class LoseRocketGameUiController : MonoBehaviour
{
    [SerializeField] private GameObject uiGameObject;
    private void Start()
    {
        RocketGame.instance.onGameLose += OnGameLose;
    }

    private void OnDestroy()
    {
        RocketGame.instance.onGameLose -= OnGameLose;
    }

    private void OnGameLose()
    {
        uiGameObject.SetActive(true);
    }

    public void OnContinueButtonClick()
    {
        RocketGame.instance.ResumeGame();
        LevelManager.instance.OpenMainMenu();
    }
    
    public void PlayAudio(AudioClip audioClip)
    {
        AudioManager.instance.PlayAudio(audioClip);
    }
}
