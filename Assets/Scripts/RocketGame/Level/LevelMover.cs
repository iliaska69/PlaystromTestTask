using System;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    private float _speed;
    private Transform _objectTransform;

    private void Start()
    {
        _speed = RocketGame.instance.LevelSpeed;
        _objectTransform = transform;
    }

    private void Update()
    {
        ModeObject();
    }

    private void ModeObject()
    {
        if(!InputManager.instance.GetScreenTapStatus()) return;

        var moveVector = Vector3.zero;
        moveVector.y += _speed * Time.deltaTime * -1;
        _objectTransform.position += moveVector;
    }
}
