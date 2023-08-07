using System;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    [SerializeField] private CircleGenerator circleGenerator;

    [SerializeField] private float radius;
    [SerializeField] private int circleQuality;


    [SerializeField] private float distanceBetweenCircles;
    private Vector3 _lastMousePosition;

    [SerializeField] private GameObject eraserGameObject;
    private Transform _eraserTransform;
    
    void Start()
    {
        _eraserTransform = eraserGameObject.transform;
        InputManager.instance.onMouseClick += OnMouseClick;
    }

    private void Update()
    {
        HandleMouseMoving();
    }

    private void HandleMouseMoving()
    {
        var mousePosition = InputManager.instance.GetMousePosition();
        if(mousePosition == Vector3.back)
        {
            eraserGameObject.SetActive(false);
            return;
        }

        if (Vector3.Distance(_lastMousePosition, mousePosition) >= distanceBetweenCircles)
        {
            eraserGameObject.SetActive(true);
            _eraserTransform.position = mousePosition;
            _lastMousePosition = mousePosition;
            circleGenerator.DrawCircle(circleQuality, radius, mousePosition);
        }
    }

    private void OnDestroy()
    {
        InputManager.instance.onMouseClick -= OnMouseClick;
    }

    private void OnMouseClick(Vector3 mousePosition)
    {
        eraserGameObject.SetActive(true);
        _eraserTransform.position = mousePosition;
        _lastMousePosition = mousePosition;
        circleGenerator.Clear();
        circleGenerator.DrawCircle(circleQuality, radius, mousePosition);
    }
}
