using UnityEngine;

public class RocketGameUiController : MonoBehaviour
{
    void Start()
    {
        RocketGame.instance.PauseGame();
    }

    public void OnContinueButtonClick()
    {
        RocketGame.instance.ResumeGame();
    }
    
    public void PlayAudio(AudioClip audioClip)
    {
        AudioManager.instance.PlayAudio(audioClip);
    }
}
