using UnityEngine;

public class MainMenuUiController : MonoBehaviour
{
    public void OnDopGameButtonClick()
    {
        LevelManager.instance.OpenLevelById(1);
    }

    public void OnRocketGameButtonClick()
    {
        LevelManager.instance.OpenLevelById(2);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        AudioManager.instance.PlayAudio(audioClip);
    }
    
}
