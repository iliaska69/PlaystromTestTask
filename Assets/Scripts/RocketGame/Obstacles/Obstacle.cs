using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var destroyableComponent = other.GetComponent<IDestroyable>();
        if(destroyableComponent == null) return;
        
        destroyableComponent.DestroyObject();
    }
}
