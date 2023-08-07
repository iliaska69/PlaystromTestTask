using UnityEngine;

public class EraserGameUiController : MonoBehaviour
{
    public void OnDemonstratePhysicsButtonClick()
    {
        EraserGameManager.instance.DemonstratePhysics();
    }

    public void OnBackToMenuButtonClick()
    {
        LevelManager.instance.OpenMainMenu();
    }
    
    public void PlayAudio(AudioClip audioClip)
    {
        AudioManager.instance.PlayAudio(audioClip);
    }
}
