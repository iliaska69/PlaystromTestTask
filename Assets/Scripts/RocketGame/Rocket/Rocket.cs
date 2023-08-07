using UnityEngine;

public class Rocket : MonoBehaviour, IDestroyable
{
    [SerializeField] private float velocity;
    
    private Transform _rocketTransform;
    private Rigidbody _rocketRigidbody;
    void Awake()
    {
        _rocketTransform = transform;
        _rocketRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        HandleMovement();
        RotateRocket();
    }

    private void RotateRocket()
    {
        
        var eulerAngles = transform.eulerAngles;

        var targetZAngle = 0;

        var currentZAngle = 0f;

        var movementVector = InputManager.instance.GetMovementVector().x;
        
        currentZAngle = Mathf.LerpAngle(eulerAngles.z, movementVector * 700, 0.05f);
        transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, currentZAngle);
    }

    private void HandleMovement()
    {
        var finalMovementVector = new Vector2();

        var distanceToZero = Mathf.Clamp(transform.position.y,-1, 0);

        distanceToZero = Mathf.Abs(distanceToZero);

        if (InputManager.instance.GetScreenTapStatus())
        {
            finalMovementVector = Vector2.up * velocity;
            var newTransform = new Vector3(_rocketTransform.position.x, _rocketTransform.position.y , 0);
            newTransform.x += InputManager.instance.GetMovementVector().x * -1;
            _rocketTransform.position = newTransform;
            _rocketRigidbody.velocity = finalMovementVector * distanceToZero;
        }
    }

    public void DestroyObject()
    {
        RocketGame.instance.LoseGame();
    }
}
