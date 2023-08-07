using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    private bool _screenTapStatus;
    private Vector2 _movementVector;
    private Vector3 _oldTouch;
    
    public delegate void OnMouseClick(Vector3 worldMousePosition);
    public event OnMouseClick onMouseClick;

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
        DontDestroyOnLoad(gameObject);
    }

    public Vector2 GetMovementVector()
    {
        return _movementVector;
    }

    public bool GetScreenTapStatus()
    {
        return _screenTapStatus;
    }

    public Vector3 GetMousePosition()
    {
        if (!Input.GetMouseButton(0) || IsMouseOverUI()) return Vector3.back;
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Update()
    {
        HandleScreenTap();
        HandleMovementVector();
        HandleMouseButtonDown();
    }

    private void HandleMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            onMouseClick?.Invoke(Camera.main.ScreenToWorldPoint(mousePosition));
        }
    }

    private void HandleScreenTap()
    {
        _screenTapStatus = Input.GetMouseButton(0);
    }

    private void HandleMovementVector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            _oldTouch = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            _movementVector = _oldTouch - Camera.main.ScreenToWorldPoint(mousePosition);
            _oldTouch = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else
        {
            _movementVector = Vector2.zero;
        }
        
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.currentSelectedGameObject != null;
    }
}
