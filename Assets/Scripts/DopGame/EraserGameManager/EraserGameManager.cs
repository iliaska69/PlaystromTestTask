using UnityEngine;

public class EraserGameManager : MonoBehaviour
{
    
    public static EraserGameManager instance = null;
    
    public delegate void OnDemonstratePhysics();
    public event OnDemonstratePhysics onDemonstratePhysics;

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

    public void DemonstratePhysics()
    {
        onDemonstratePhysics?.Invoke();
    }
}
