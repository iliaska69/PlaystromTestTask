using UnityEngine;

public class RocketGame : MonoBehaviour
{
    [SerializeField] private float levelSpeed;

    public float LevelSpeed => levelSpeed;
    
    public static RocketGame instance = null;

    public delegate void OnGameLose();
    public event OnGameLose onGameLose;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    public void LoseGame()
    {
        onGameLose?.Invoke();
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
