using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> balls;
    void Start()
    {
        EraserGameManager.instance.onDemonstratePhysics += SpawnBalls;
    }

    private void OnDestroy()
    {
        EraserGameManager.instance.onDemonstratePhysics -= SpawnBalls;
    }

    private void SpawnBalls()
    {
        foreach (var ball in balls)
        {
            ball.SetActive(true);
        }
    }
    
}
